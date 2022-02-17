using System;
using System.Runtime.InteropServices;
using SardineFish.Utils;

public class TransparentWindowManager : SingletonMonoBehaviour<TransparentWindowManager>
{
    public bool transparent = false;
    public bool borderless = false;
    public bool clickThrough = false;
    public bool topMost = false;

    private bool _internalTransparent = false;
    private bool _internalBorderless = false;
    private bool _internalClickThrough = false;
    private bool _internalTopMost = false;
    
    #region Enum

    internal enum WindowCompositionAttribute
    {
        WCA_ACCENT_POLICY = 19
    }

    internal enum AccentState
    {
        ACCENT_DISABLED = 0,
        ACCENT_ENABLE_GRADIENT = 1,
        ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
        ACCENT_ENABLE_BLURBEHIND = 3,
        ACCENT_INVALID_STATE = 4
    }

    #endregion Enum

    #region Struct

    private struct MARGINS
    {
        public int cxLeftWidth;
        public int cxRightWidth;
        public int cyTopHeight;
        public int cyBottomHeight;
    }

    internal struct WindowCompositionAttributeData
    {
        public WindowCompositionAttribute Attribute;
        public IntPtr Data;
        public int SizeOfData;
    }

    internal struct AccentPolicy
    {
        public AccentState AccentState;
        public int AccentFlags;
        public int GradientColor;
        public int AnimationId;
    }

    #endregion Struct

    #region DLL Import

    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);
    
    [DllImport("user32.dll")]
    private static extern int SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

    [DllImport("user32.dll")]
    private static extern int SetLayeredWindowAttributes(IntPtr hWnd, uint crKey, byte bAlpha, uint dwFlags);

    [DllImport("Dwmapi.dll")]
    private static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);
    
    [DllImport("user32.dll")]
    internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);
    

    #endregion DLL Import

    #region Method

    // CAUTION:
    // To control enable or disable, use Start method instead of Awake.

    protected virtual void Start()
    {
        UpdateWindow();
    }

    private void Update()
    {
        if (_internalBorderless != borderless || _internalTransparent != transparent || clickThrough != _internalClickThrough)
        {
            UpdateWindow();
        }
    }

    private void UpdateWindow()
    {
#if !UNITY_EDITOR && UNITY_STANDALONE_WIN


        const int GWL_EXSTYLE = -20;
        const uint WS_EX_LAYERD = 0x080000;
        const uint WS_EX_TRANSPARENT = 0x00000020;
        const uint WS_EX_LEFT = 0x00000000;
        const uint WS_EX_NOREDIRECTIONBITMAP = 0x00200000;  
        
        const int  GWL_STYLE = -16;
        const uint WS_POPUP = 0x80000000;
        const uint WS_VISIBLE = 0x10000000;
        
        const uint LWA_ALPHA = 0x00000002;
        const uint LWA_COLORKEY = 0x00000001;

        const long HWND_TOPMOST = -1;
        const uint SWP_NOMOVE = 0x0002;
        const uint SWP_NOSIZE = 0x0001;
        
        var windowHandle = GetActiveWindow();
        
        MARGINS margins = new MARGINS()
        {
            cxLeftWidth = -1
        };
        
        DwmExtendFrameIntoClientArea(windowHandle, ref margins);
        uint flag = 0;
        if (transparent)
            flag |= WS_EX_LAYERD;
        if (clickThrough)
            flag |= WS_EX_TRANSPARENT;
        SetWindowLong(windowHandle, GWL_EXSTYLE, flag);

        SetWindowPos(windowHandle, (IntPtr)HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);

        _internalBorderless = borderless;
        _internalTransparent = transparent;
        _internalClickThrough = clickThrough;
#endif // !UNITY_EDITOR && UNITY_STANDALONE_WIN
    }

    #endregion Method
}