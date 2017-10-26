using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Facepunch.Steamworks
{
    public partial class Client : IDisposable
    {
        private HTMLSurface _htmlSurface;

        public HTMLSurface htmlSurface
        {
            get
            {
                if (_htmlSurface == null )
					_htmlSurface = new HTMLSurface { client = this };

                return _htmlSurface;
            }
        }
    }

    public class HTMLSurface
    {
        internal Client client;

		internal SteamNative.SteamHTMLSurface htmlSurface;
		private SteamNative.HHTMLBrowser mBrowser;
		private bool mReady = false;

		public delegate void Func();
		public delegate void Func<T1>(T1 arg1);
		public delegate void Func<T1, T2>(T1 arg1, T2 arg2);
		public delegate void Func<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3);
		public delegate void Func<T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
		public delegate void Func<T1, T2, T3, T4, T5>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
		public delegate void Func<T1, T2, T3, T4, T5, T6>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6);
		public delegate void Func<T1, T2, T3, T4, T5, T6, T7>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7);
		public delegate void Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11);

		public Func<UInt32> onBrowserReady;
		public Func<bool, bool> onCanGoBackAndForward;
		public Func<string> onChangedTitle;
		public Func onCloseBrowser;
		public Func<string, string> onFileOpenDialog;
		public Func<string, string> onFinishedRequest;
		public Func onHideToolTip;
		public Func<UInt32, UInt32, float, bool, UInt32> onHorizontalScroll;
		public Func<string> onJSAlert;
		public Func<string> onJSConfirm;
		public Func<UInt32, UInt32, string, bool, bool> onLinkAtPosition;
		public Func<IntPtr, UInt32, UInt32, UInt32, UInt32, UInt32, UInt32, UInt32, UInt32, float, UInt32> onNeedsPaint;
		public Func<string, UInt32, UInt32, UInt32, UInt32, UInt32> onNewWindow;
		public Func<string> onOpenLinkInNewTab;
		public Func<UInt32, UInt32> onSearchResults;
		public Func<UInt32> onSetCursor;
		public Func<string> onShowToolTip;
		public Func<string, string, string, bool> onStartRequest;
		public Func<string> onStatusText;
		public Func<string> onUpdateToolTip;
		public Func<string, string, bool, string, bool> onURLChanged;
		public Func<UInt32, UInt32, float, bool, UInt32> onVerticalScroll;

		public void Init() {
			htmlSurface = client.native.htmlSurface;
			htmlSurface.Init();

			//SteamNative.HTML_BrowserReady_t.RegisterCallback(client, HTML_BrowserReady_t_CB);
			SteamNative.HTML_CanGoBackAndForward_t.RegisterCallback(client, HTML_CanGoBackAndForward_t_CB);
			SteamNative.HTML_ChangedTitle_t.RegisterCallback(client, HTML_ChangedTitle_t_CB);
			//SteamNative.HTML_CloseBrowser_t.RegisterCallback(client, HTML_CloseBrowser_t_CB);
			SteamNative.HTML_FileOpenDialog_t.RegisterCallback(client, HTML_FileOpenDialog_t_CB);
			SteamNative.HTML_FinishedRequest_t.RegisterCallback(client, HTML_FinishedRequest_t_CB);
			SteamNative.HTML_HideToolTip_t.RegisterCallback(client, HTML_HideToolTip_t_CB);
			SteamNative.HTML_HorizontalScroll_t.RegisterCallback(client, HTML_HorizontalScroll_t_CB);
			SteamNative.HTML_JSAlert_t.RegisterCallback(client, HTML_JSAlert_t_CB);
			SteamNative.HTML_JSConfirm_t.RegisterCallback(client, HTML_JSConfirm_t_CB);
			SteamNative.HTML_LinkAtPosition_t.RegisterCallback(client, HTML_LinkAtPosition_t_CB);
			SteamNative.HTML_NeedsPaint_t.RegisterCallback(client, HTML_NeedsPaint_t_CB);
			SteamNative.HTML_NewWindow_t.RegisterCallback(client, HTML_NewWindow_t_CB);
			SteamNative.HTML_OpenLinkInNewTab_t.RegisterCallback(client, HTML_OpenLinkInNewTab_t_CB);
			SteamNative.HTML_SearchResults_t.RegisterCallback(client, HTML_SearchResults_t_CB);
			SteamNative.HTML_SetCursor_t.RegisterCallback(client, HTML_SetCursor_t_CB);
			SteamNative.HTML_ShowToolTip_t.RegisterCallback(client, HTML_ShowToolTip_t_CB);
			//SteamNative.HTML_StartRequest_t.RegisterCallback(client, HTML_StartRequest_t_CB);
			SteamNative.HTML_StatusText_t.RegisterCallback(client, HTML_StatusText_t_CB);
			SteamNative.HTML_UpdateToolTip_t.RegisterCallback(client, HTML_UpdateToolTip_t_CB);
			SteamNative.HTML_URLChanged_t.RegisterCallback(client, HTML_URLChanged_t_CB);
			SteamNative.HTML_VerticalScroll_t.RegisterCallback(client, HTML_VerticalScroll_t_CB);
		}
		public void CreateBrowser(string userAgent, string css) {
			if(mReady) return;

			htmlSurface.CreateBrowser(userAgent, css, HTML_BrowserReady_t_CB);
		}
		public void SetHorizontalScroll(uint absScrollPos) {
			if(!mReady) return;

			htmlSurface.SetHorizontalScroll(mBrowser, absScrollPos);
		}
		public void GetLinkAtPosition(int x, int y) {
			if(!mReady) return;

			htmlSurface.GetLinkAtPosition(mBrowser, x, y);
		}
		public void Find(string searchString, bool currentlyInFind, bool reverse) {
			if(!mReady) return;

			htmlSurface.Find(mBrowser, searchString, currentlyInFind, reverse);
		}
		public void LoadURL(string url, string postData) {
			if(!mReady) {
				return;
			}
			
			htmlSurface.LoadURL(mBrowser, url, postData);
		}
		public void SetVerticalScroll(uint absScrollPos) {
			if(!mReady) return;

			htmlSurface.SetVerticalScroll(mBrowser, absScrollPos);
		}
		public void AllowStartRequest(bool allowed) {
			if(!mReady) return;

			htmlSurface.AllowStartRequest(mBrowser, allowed);
		}
		public void Shutdown() {
			if(!mReady) return;

			htmlSurface.RemoveBrowser(mBrowser);

			htmlSurface.Shutdown();
			mBrowser = 0;
			mReady = false;
			htmlSurface = null;
		}

		public void SetSize(uint width, uint height) {
			if(!mReady) return;

			htmlSurface.SetSize(mBrowser, width, height);
		}
		public void SetPageScaleFactor(float zoom) {
			if(!mReady) return;

			htmlSurface.SetPageScaleFactor(mBrowser, zoom, 0, 0);
		}
		public void MouseDown(uint mouseID) {
			if(!mReady) return;

			htmlSurface.MouseDown(mBrowser, (SteamNative.HTMLMouseButton) mouseID);
		}
		public void MouseUp(uint mouseID) {
			if(!mReady) return;

			htmlSurface.MouseUp(mBrowser, (SteamNative.HTMLMouseButton) mouseID);
		}
		public void MouseWheel(int delta) {
			if(!mReady) return;

			htmlSurface.MouseWheel(mBrowser, delta);
		}
		public void MouseMove(int x, int y) {
			if(!mReady) return;

			htmlSurface.MouseMove(mBrowser, x, y);
		}
		public void KeyDown(uint keyCode) {
			if(!mReady) return;

			htmlSurface.KeyDown(mBrowser, keyCode, SteamNative.HTMLKeyModifiers.None);
		}
		public void KeyUp(uint keyCode) {
			if(!mReady) return;

			htmlSurface.KeyUp(mBrowser, keyCode, SteamNative.HTMLKeyModifiers.None);
		}
		public void KeyChar(uint keyChar) {
			if(!mReady) return;

			htmlSurface.KeyChar(mBrowser, keyChar, SteamNative.HTMLKeyModifiers.None);
		}

		public void JSDialogResponse(bool result) {
			if(!mReady) return;

			htmlSurface.JSDialogResponse(mBrowser, result);
		}


		void HTML_BrowserReady_t_CB(SteamNative.HTML_BrowserReady_t data, bool failed) {
			mBrowser = data.UnBrowserHandle;
			mReady = true;

			if(onBrowserReady == null) return;

			onBrowserReady(mBrowser);
		}
		void HTML_CanGoBackAndForward_t_CB(SteamNative.HTML_CanGoBackAndForward_t data, bool failed) {
			if(onCanGoBackAndForward == null) return;

			onCanGoBackAndForward(data.BCanGoBack, data.BCanGoForward);
		}
		void HTML_ChangedTitle_t_CB(SteamNative.HTML_ChangedTitle_t data, bool failed) {
			if(onChangedTitle == null) return;

			onChangedTitle(data.PchTitle);
		}
		void HTML_CloseBrowser_t_CB(SteamNative.HTML_CloseBrowser_t data, bool failed) {
			if(onCloseBrowser == null) return;

			onCloseBrowser();
		}
		void HTML_FileOpenDialog_t_CB(SteamNative.HTML_FileOpenDialog_t data, bool failed) {
			if(onFileOpenDialog == null) return;

			onFileOpenDialog(data.PchTitle, data.PchInitialFile);
		}
		void HTML_FinishedRequest_t_CB(SteamNative.HTML_FinishedRequest_t data, bool failed) {
			if(onFinishedRequest == null) return;

			onFinishedRequest(data.PchURL, data.PchPageTitle);
		}
		void HTML_HideToolTip_t_CB(SteamNative.HTML_HideToolTip_t data, bool failed) {
			if(onHideToolTip == null) return;

			onHideToolTip();
		}
		void HTML_HorizontalScroll_t_CB(SteamNative.HTML_HorizontalScroll_t data, bool failed) {
			if(onHorizontalScroll == null) return;

			onHorizontalScroll(data.UnScrollMax, data.UnScrollCurrent, data.FlPageScale, data.BVisible, data.UnPageSize);
		}
		void HTML_JSAlert_t_CB(SteamNative.HTML_JSAlert_t data, bool failed) {
			if(onJSAlert == null) return;

			onJSAlert(data.PchMessage);
		}
		void HTML_JSConfirm_t_CB(SteamNative.HTML_JSConfirm_t data, bool failed) {
			if(onJSConfirm == null) return;

			onJSConfirm(data.PchMessage);
		}
		void HTML_LinkAtPosition_t_CB(SteamNative.HTML_LinkAtPosition_t data, bool failed) {
			if(onLinkAtPosition == null) return;

			onLinkAtPosition(data.X, data.Y, data.PchURL, data.BInput, data.BLiveLink);
		}
		void HTML_NeedsPaint_t_CB(SteamNative.HTML_NeedsPaint_t data, bool failed) {
			if(onNeedsPaint == null) return;

			onNeedsPaint(data.PBGRA, data.UnWide, data.UnTall, data.UnUpdateX, data.UnUpdateY, data.UnUpdateWide, data.UnUpdateTall, data.UnScrollX, data.UnScrollY, data.FlPageScale, data.UnPageSerial);
		}
		void HTML_NewWindow_t_CB(SteamNative.HTML_NewWindow_t data, bool failed) {
			if(onNewWindow == null) return;

			onNewWindow(data.PchURL, data.UnX, data.UnY, data.UnWide, data.UnTall, data.UnNewWindow_BrowserHandle);
		}
		void HTML_OpenLinkInNewTab_t_CB(SteamNative.HTML_OpenLinkInNewTab_t data, bool failed) {
			if(onOpenLinkInNewTab == null) return;

			onOpenLinkInNewTab(data.PchURL);
		}
		void HTML_SearchResults_t_CB(SteamNative.HTML_SearchResults_t data, bool failed) {
			if(onSearchResults == null) return;

			onSearchResults(data.UnResults, data.UnCurrentMatch);
		}
		void HTML_SetCursor_t_CB(SteamNative.HTML_SetCursor_t data, bool failed) {
			if(onSetCursor == null) return;

			onSetCursor(data.EMouseCursor);
		}
		void HTML_ShowToolTip_t_CB(SteamNative.HTML_ShowToolTip_t data, bool failed) {
			if(onShowToolTip == null) return;

			onShowToolTip(data.PchMsg);
		}
		void HTML_StartRequest_t_CB(SteamNative.HTML_StartRequest_t data, bool failed) {
			if(onStartRequest == null) return;

			onStartRequest(data.PchURL, data.PchTarget, data.PchPostData, data.BIsRedirect);
		}
		void HTML_StatusText_t_CB(SteamNative.HTML_StatusText_t data, bool failed) {
			if(onStatusText == null) return;

			onStatusText(data.PchMsg);
		}
		void HTML_UpdateToolTip_t_CB(SteamNative.HTML_UpdateToolTip_t data, bool failed) {
			if(onUpdateToolTip == null) return;

			onUpdateToolTip(data.PchMsg);
		}
		void HTML_URLChanged_t_CB(SteamNative.HTML_URLChanged_t data, bool failed) {
			if(onURLChanged == null) return;

			onURLChanged(data.PchURL, data.PchPostData, data.BIsRedirect, data.PchPageTitle, data.BNewNavigation);
		}
		void HTML_VerticalScroll_t_CB(SteamNative.HTML_VerticalScroll_t data, bool failed) {
			if(onVerticalScroll == null) return;

			onVerticalScroll(data.UnScrollMax, data.UnScrollCurrent, data.FlPageScale, data.BVisible, data.UnPageSize);
		}
	}
}
