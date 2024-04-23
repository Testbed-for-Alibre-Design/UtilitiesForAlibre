﻿using System;
using System.Collections.Generic;
using AlibreAddOn;
using AlibreX;
using Bolsover.AlibreDataViewer;
using Bolsover.Bevel.Views;
using Bolsover.CycloidalGear;
using Bolsover.DataBrowser;
using Bolsover.FaceArea;
using Bolsover.Involute.View;
using Bolsover.PlaneFinder;
using Bolsover.RackPinion.View;
using Bolsover.Shortcuts.View;
using Bolsover.WormGear.View;
using Shortcuts.Shortcuts.View;

namespace Bolsover
{
    public class UtilitiesForAlibre : IAlibreAddOn
    {
        private const int MenuIdRoot = 401;

        private const int SubmenuIdDataBrowser = 505;
        private const int MenuIdGear = 506;
        private const int MenuIdUtils = 601;
        private const int SubMenuIdShortcuts = 605;
        private const int SubmenuIdGearCycloidal = 602;
        private const int SubmenuIdGearBevel = 606;
        private const int SubmenuIdUtilsPlaneFinder = 603;
        private const int SubmenuIdUtilsDataViewer = 604;
        
        private const int SubmenuIdGearRackSpur = 607;
        private const int SubmenuIdGearWormGear = 608;
        private const int SubmenuIdGearInvolute = 609;
        private const int SubmenuIdShortcutsReport = 610;
        private const int SubmenuIdShortcutsKeyboard = 611;
        private const int SubmenuIdUtilsFaceArea = 612;
        private const int MenuIdHelp = 701;
        private const int SubmenuIdHelpAbout = 702;
        private int[] _menuIdsUtils;
        private int[] _menuIdsShortcuts;
        private int[] _menuIdsRoot;
        private int[] _menuIdsHelp;
        private int[] _menuIdsGear;

        private readonly IADRoot _alibreRoot;
        private IntPtr _parentWinHandle;

        public UtilitiesForAlibre(IADRoot alibreRoot, IntPtr parentWinHandle)
        {
            _alibreRoot = alibreRoot;
            _parentWinHandle = parentWinHandle;
            BuildMenuTree();
        }

        #region Menus

        /// <summary>
        /// Returns the menu ID of the add-on root menu item
        /// </summary>
        public int RootMenuItem
        {
            get => MenuIdRoot;
        }

        /// <summary>
        /// Builds the menu tree
        /// </summary>
        private void BuildMenuTree()
        {
            _menuIdsShortcuts = new[]
            {
                SubmenuIdShortcutsReport,
                SubmenuIdShortcutsKeyboard
            };

            _menuIdsUtils = new[]
            {
                SubMenuIdShortcuts,
                SubmenuIdDataBrowser,
                SubmenuIdUtilsPlaneFinder,
                SubmenuIdUtilsDataViewer
             //   SubmenuIdUtilsFaceArea
            };
            _menuIdsRoot = new[]
            {
                MenuIdUtils,
                MenuIdGear,
                MenuIdHelp
            };
            _menuIdsHelp = new[]
            {
                SubmenuIdHelpAbout
            };

            _menuIdsGear = new[]
            {
                SubmenuIdGearCycloidal,
                SubmenuIdGearInvolute,
                SubmenuIdGearBevel,
                SubmenuIdGearRackSpur,
                SubmenuIdGearWormGear
            };
        }

        /// <summary>
        /// Description("Returns Whether the given Menu ID has any sub menus")
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public bool HasSubMenus(int menuId)
        {
            return menuId switch
            {
                MenuIdRoot => true,
                MenuIdUtils => true,
                MenuIdHelp => true,
                MenuIdGear => true,
                SubMenuIdShortcuts => true,
                _ => false
            };
        }

        /// <summary>
        /// Returns the ID's of sub menu items under a popup menu item; the menu ID of a 'leaf' menu becomes its command ID
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public Array SubMenuItems(int menuId)
        {
            return menuId switch
            {
                MenuIdRoot => _menuIdsRoot,
                MenuIdGear => _menuIdsGear,
                MenuIdUtils => _menuIdsUtils,
                MenuIdHelp => _menuIdsHelp,
                SubMenuIdShortcuts => _menuIdsShortcuts,
                _ => null
            };
        }

        /// <summary>
        /// Returns the display name of a menu item; a menu item with text of a single dash (“-“) is a separator
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public string MenuItemText(int menuId)
        {
            return menuId switch
            {
                MenuIdRoot => "Utilities",
                MenuIdUtils => "Utilities",
                MenuIdHelp => "Help",
                MenuIdGear => "Gears",
                SubmenuIdDataBrowser => "Data Browser",
                SubmenuIdGearCycloidal => "Cycloidal Gears Open/Close",
                SubmenuIdGearInvolute => "Involute Spur & Helical Gears",
                SubmenuIdGearBevel => "Bevel Gears",
                SubmenuIdHelpAbout => "About",
                SubmenuIdUtilsPlaneFinder => "Sketch Plane Finder Open/Close",
                SubmenuIdUtilsDataViewer => "Property Viewer Open/Close",
                SubMenuIdShortcuts => "Shortcuts",
                SubmenuIdShortcutsReport => "Shortcuts Report",
                SubmenuIdShortcutsKeyboard => "Keyboard layout",
                SubmenuIdGearRackSpur => "Rack and Pinion",
                SubmenuIdGearWormGear => "Worm & Worm Gear",
                SubmenuIdUtilsFaceArea => "Face Area",
                _ => ""
            };
        }

        /// <summary>
        /// Returns True if input menu item has sub menus // seems odd given name of method
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public bool PopupMenu(int menuId)
        {
            return false;
        }

        /// <summary>
        /// Returns property bits providing information about the state of a menu item
        /// ADDON_MENU_ENABLED = 1,
        /// ADDON_MENU_GRAYED = 2,
        /// ADDON_MENU_CHECKED = 3,
        /// ADDON_MENU_UNCHECKED = 4,
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="sessionIdentifier"></param>
        /// <returns></returns>
        public ADDONMenuStates MenuItemState(int menuId, string sessionIdentifier)
        {
            var session = _alibreRoot.Sessions.Item(sessionIdentifier);

            switch (session)
            {
                case IADDrawingSession:
                    switch (menuId)
                    {
                        case MenuIdRoot: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case MenuIdUtils: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case MenuIdGear: return ADDONMenuStates.ADDON_MENU_GRAYED;
                        case SubmenuIdDataBrowser: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case SubmenuIdGearCycloidal: return ADDONMenuStates.ADDON_MENU_GRAYED;
                        case SubmenuIdGearInvolute: return ADDONMenuStates.ADDON_MENU_GRAYED;
                        case SubmenuIdGearBevel: return ADDONMenuStates.ADDON_MENU_GRAYED;
                        case SubmenuIdUtilsPlaneFinder: return ADDONMenuStates.ADDON_MENU_GRAYED;
                        case SubmenuIdUtilsDataViewer: return ADDONMenuStates.ADDON_MENU_GRAYED;
                        case SubmenuIdHelpAbout: return ADDONMenuStates.ADDON_MENU_GRAYED;
                        case SubmenuIdShortcutsReport: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case SubmenuIdShortcutsKeyboard: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case SubMenuIdShortcuts: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case SubmenuIdGearRackSpur: return ADDONMenuStates.ADDON_MENU_GRAYED;
                        case SubmenuIdGearWormGear: return ADDONMenuStates.ADDON_MENU_GRAYED;
                        case  SubmenuIdUtilsFaceArea: return ADDONMenuStates.ADDON_MENU_GRAYED;
                    }

                    break;

                case IADAssemblySession:
                    switch (menuId)
                    {
                        case MenuIdRoot: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case MenuIdUtils: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case MenuIdGear: return ADDONMenuStates.ADDON_MENU_GRAYED;
                        case SubmenuIdDataBrowser: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case SubmenuIdGearCycloidal: return ADDONMenuStates.ADDON_MENU_GRAYED;
                        case SubmenuIdGearInvolute: return ADDONMenuStates.ADDON_MENU_GRAYED;
                        case SubmenuIdGearBevel: return ADDONMenuStates.ADDON_MENU_GRAYED;
                        case SubmenuIdUtilsPlaneFinder: return ADDONMenuStates.ADDON_MENU_GRAYED;
                        case SubmenuIdUtilsDataViewer: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case SubmenuIdHelpAbout: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case SubmenuIdShortcutsReport: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case SubmenuIdShortcutsKeyboard: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case SubMenuIdShortcuts: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case SubmenuIdGearRackSpur: return ADDONMenuStates.ADDON_MENU_GRAYED;
                        case SubmenuIdGearWormGear: return ADDONMenuStates.ADDON_MENU_GRAYED;
                        case  SubmenuIdUtilsFaceArea: return ADDONMenuStates.ADDON_MENU_GRAYED;
                    }

                    break;
                case IADPartSession:
                    switch (menuId)
                    {
                        case MenuIdRoot: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case MenuIdUtils: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case MenuIdGear: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case SubmenuIdDataBrowser: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case SubmenuIdGearCycloidal: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case SubmenuIdGearBevel: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case SubmenuIdGearInvolute: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case SubmenuIdUtilsPlaneFinder: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case SubmenuIdUtilsDataViewer: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case SubmenuIdHelpAbout: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case SubmenuIdShortcutsReport: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case SubmenuIdShortcutsKeyboard: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case SubMenuIdShortcuts: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case SubmenuIdGearRackSpur: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case SubmenuIdGearWormGear: return ADDONMenuStates.ADDON_MENU_ENABLED;
                        case  SubmenuIdUtilsFaceArea: return ADDONMenuStates.ADDON_MENU_ENABLED;
                    }

                    break;
            }

            return ADDONMenuStates.ADDON_MENU_GRAYED;
        }

        /// <summary>
        /// Returns a tool tip string if input menu ID is that of a 'leaf' menu item
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public string MenuItemToolTip(int menuId)
        {
            return menuId switch
            {
                MenuIdRoot => "Utilities",
                MenuIdUtils => "Utilities",
                MenuIdGear => "Gears",
                SubmenuIdDataBrowser => "Opens the custom Data Browser",
                SubmenuIdGearCycloidal => "Opens/Closes Cycloidal Gear Generator",
                SubmenuIdGearBevel => "Opens the Bevel Gear Generator",
                SubmenuIdGearInvolute => "Opens Spur and Helical Gear Generator",
                SubmenuIdUtilsPlaneFinder => "Finds the Plane on which a selected Sketch is drawn",
                SubmenuIdUtilsDataViewer => "Opens/Closes Property Viewer",
                SubmenuIdHelpAbout => "About Utilities for Alibre",
                SubmenuIdShortcutsReport => "Shortcuts Report",
                SubmenuIdShortcutsKeyboard => "Shortcuts Keyboard",
                SubMenuIdShortcuts => "Shortcuts",
                SubmenuIdGearRackSpur => "Rack & Pinion",
                SubmenuIdGearWormGear => "Worm & Worm Gear",
                SubmenuIdUtilsFaceArea => "Face Area",
                _ => ""
            };
        }

        /// <summary>
        /// Returns the icon name (with extension) for a menu item; the icon will be searched under the folder where the add-on .adc file is present
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public string MenuIcon(int menuId)
        {
            // if you want icons, reference them here...
            // switch (menuID)
            // {
            //     case SUBMENU_ID_HELP_ABOUT: return "icons/userquestion.ico";
            //     case SUBMENU_ID_DATA_BROWSER: return "icons/search.ico";
            //     case SUBMENU_ID_FILE_OPEN: return "icons/file-code-add.ico";
            //     case SUBMENU_ID_FILE_CLOSE: return "icons/file-code-star.ico";
            //     case SUBMENU_ID_FILE_EXIT: return "icons/file-code-question.ico";
            //     case SUBMENU_ID_UTILS_CYCLOIDAL_GEAR: return "icons/cog-double.ico";
            //     case SUBMENU_ID_UTILS_PLANE_FINDER: return "";
            //     case SUBMENU_ID_UTILS_DATA_VIEWER: return "";
            // }

            return string.Empty;
        }

        /// <summary>
        /// Returns True if AddOn has updated Persistent Data
        /// </summary>
        /// <param name="sessionIdentifier"></param>
        /// <returns></returns>
        public bool HasPersistentDataToSave(string sessionIdentifier)
        {
            return false;
        }

        /// <summary>
        /// Invokes the add-on command identified by menu ID; returning the add-on command interface is optional
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="sessionIdentifier"></param>
        /// <returns></returns>
        public IAlibreAddOnCommand InvokeCommand(int menuId, string sessionIdentifier)
        {
            var session = _alibreRoot.Sessions.Item(sessionIdentifier);

            return menuId switch
            {
                SubmenuIdDataBrowser => DoDataBrowser(),
                SubmenuIdGearCycloidal => DoCycloidalGear(session),
                SubmenuIdGearInvolute => DoAdvancedGear(),
                SubmenuIdGearBevel => DoBevel(),
                SubmenuIdUtilsPlaneFinder => DoPlaneFinder(session),
                SubmenuIdUtilsDataViewer => DoAlibreDataViewer(session),
                SubmenuIdHelpAbout => DoHelpAbout(),
                SubmenuIdShortcutsReport => DoShortcuts(),
                SubmenuIdShortcutsKeyboard => DoKeyboard(),
                SubmenuIdGearRackSpur => DoRackSpur(),
                SubmenuIdGearWormGear => DoWormGear(),
                SubmenuIdUtilsFaceArea => DoFaceArea(session),
                _ => null
            };
        }

        #endregion

        #region Shortcuts

        private KeyboardForm _keyboardForm;

        private IAlibreAddOnCommand DoKeyboard()
        {
            if (_keyboardForm == null)
            {
                _keyboardForm = KeyboardForm.Instance();
                _keyboardForm.keyboardControl1.ProfileComboBox.SelectedIndex = 0;
                _keyboardForm.TopMost = true;
            }
            else
            {
                _keyboardForm.Visible = true;
                _keyboardForm.TopMost = true;
            }

            return null;
        }


        private KeyboardShortcutForm _keyboardShortcutForm;

        private IAlibreAddOnCommand DoShortcuts()
        {
            if (_keyboardShortcutForm == null)
            {
                _keyboardShortcutForm = KeyboardShortcutForm.Instance();
                _keyboardShortcutForm.comboBox1.SelectedIndex = 0;
                _keyboardShortcutForm.TopMost = true;
            }
            else
            {
                _keyboardShortcutForm.Visible = true;
                _keyboardShortcutForm.TopMost = true;
            }

            return null;
        }

        #endregion

        #region DataViewer

        /// <summary>
        /// A dictionary to keep track of currently open AlibreDataViewerAddOnCommand object.
        /// </summary>
        private readonly Dictionary<string, AlibreDataViewerAddOnCommand> _dataViewerAddOnCommands = new();

        /// <summary>
        /// Toggles the viewer on/off
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        private IAlibreAddOnCommand DoAlibreDataViewer(IADSession session)
        {
            AlibreDataViewerAddOnCommand alibreDataViewerAddOnCommand;
            if (_dataViewerAddOnCommands.ContainsKey(session.Identifier))
            {
                if (!_dataViewerAddOnCommands.TryGetValue(session.Identifier, out alibreDataViewerAddOnCommand)) return null;
                alibreDataViewerAddOnCommand.UserRequestedClose();
                _dataViewerAddOnCommands.Remove(session.Identifier);
                return null;
            }
            else
            {
                alibreDataViewerAddOnCommand = new AlibreDataViewerAddOnCommand(session)
                {
                    AlibreDataViewer =
                    {
                        Visible = true
                    }
                };
                alibreDataViewerAddOnCommand.Terminate += AlibreDataViewerAddOnCommandOnTerminate;
                _dataViewerAddOnCommands.Add(session.Identifier, alibreDataViewerAddOnCommand);
            }

            return alibreDataViewerAddOnCommand;
        }

        private void AlibreDataViewerAddOnCommandOnTerminate(object sender,
            AlibreDataViewerAddOnCommandTerminateEventArgs e)
        {
            if (_dataViewerAddOnCommands.TryGetValue(e.AlibreDataViewerAddOnCommand.Session.Identifier,
                    out _))
            {
                _dataViewerAddOnCommands.Remove(e.AlibreDataViewerAddOnCommand.Session.Identifier);
            }
        }

        #endregion

        #region PlaneFinder

        /// <summary>
        /// A dictionary to keep track of currently open PlaneFinderAddOnCommand object.
        /// </summary>
        private readonly Dictionary<string, PlaneFinderAddOnCommand> _planeFinderAddOnCommands = new();

        private IAlibreAddOnCommand DoPlaneFinder(IADSession session)
        {
            PlaneFinderAddOnCommand planeFinderAddOnCommand;
            if (_planeFinderAddOnCommands.ContainsKey(session.Identifier))
            {
                if (!_planeFinderAddOnCommands.TryGetValue(session.Identifier, out planeFinderAddOnCommand))
                    return null;
                planeFinderAddOnCommand.UserRequestedClose();
                _planeFinderAddOnCommands.Remove(session.Identifier);
                return null;
            }
            else
            {
                planeFinderAddOnCommand = new PlaneFinderAddOnCommand(session)
                {
                    PlaneFinder =
                    {
                        Visible = true
                    }
                };
                planeFinderAddOnCommand.Terminate += PlaneFinderAddOnCommandOnTerminate;
                _planeFinderAddOnCommands.Add(session.Identifier, planeFinderAddOnCommand);
            }

            return planeFinderAddOnCommand;
        }

        private void PlaneFinderAddOnCommandOnTerminate(object sender, PlaneFinderAddOnCommandTerminateEventArgs e)
        {
            if (_planeFinderAddOnCommands.TryGetValue(e.PlaneFinderAddOnCommand.Session.Identifier,
                    out _))
            {
                _planeFinderAddOnCommands.Remove(e.PlaneFinderAddOnCommand.Session.Identifier);
            }
        }

        #endregion
        
        #region FaceArea
        
        /// <summary>
        /// A dictionary to keep track of currently open PlaneFinderAddOnCommand object.
        /// </summary>
        private readonly Dictionary<string, FaceAreaAddOnCommand> _FaceAreaAddOnCommands = new();
        
        private IAlibreAddOnCommand DoFaceArea(IADSession session)
        {
            FaceAreaAddOnCommand faceAreaAddOnCommand;
            if (_FaceAreaAddOnCommands.ContainsKey(session.Identifier))
            {
                if (!_FaceAreaAddOnCommands.TryGetValue(session.Identifier, out faceAreaAddOnCommand))
                    return null;
                faceAreaAddOnCommand.UserRequestedClose();
                _FaceAreaAddOnCommands.Remove(session.Identifier);
                return null;
            }
            else
            {
                faceAreaAddOnCommand = new FaceAreaAddOnCommand(session)
                {
                    FaceArea =
                    {
                        Visible = true
                    }
                };
                faceAreaAddOnCommand.Terminate += FaceAreaAddOnCommandOnTerminate;
                _FaceAreaAddOnCommands.Add(session.Identifier, faceAreaAddOnCommand);
            }
            
            return faceAreaAddOnCommand;
        }
        
        private void FaceAreaAddOnCommandOnTerminate(object sender, FaceAreaAddOnCommandTerminateEventArgs e)
        {
            if (_FaceAreaAddOnCommands.TryGetValue(e.FaceAreaAddOnCommand.Session.Identifier,
                    out _))
            {
                _FaceAreaAddOnCommands.Remove(e.FaceAreaAddOnCommand.Session.Identifier);
            }
        }
        
        #endregion

        #region CycloidalGear

        /// <summary>
        /// A dictionary to keep track of currently open AlibreDataViewerAddOnCommand object.
        /// </summary>
        private readonly Dictionary<string, CycloidalGearAddOnCommand> _cycloidalGearAddOnCommands = new();

        /// <summary>
        /// Opens the Cycloidal Gear generator dialog.
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        private IAlibreAddOnCommand DoCycloidalGear(IADSession session)
        {
            CycloidalGearAddOnCommand cycloidalGearAddOnCommand;
            if (!_cycloidalGearAddOnCommands.ContainsKey(session.Identifier))
            {
                cycloidalGearAddOnCommand = new CycloidalGearAddOnCommand(session)
                {
                    CycliodalGearParametersForm =
                    {
                        Visible = true
                    }
                };
                cycloidalGearAddOnCommand.Terminate += CycloidalGearAddOnCommandOnTerminate;
                _cycloidalGearAddOnCommands.Add(session.Identifier, cycloidalGearAddOnCommand);
            }
            else
            {
                if (!_cycloidalGearAddOnCommands.TryGetValue(session.Identifier, out cycloidalGearAddOnCommand)) return null;
                cycloidalGearAddOnCommand.UserRequestedClose();
                _cycloidalGearAddOnCommands.Remove(session.Identifier);
                return null;
            }

            return cycloidalGearAddOnCommand;
        }

        private void CycloidalGearAddOnCommandOnTerminate(object sender, CycloidalGearAddOnCommandTerminateEventArgs e)
        {
            if (_cycloidalGearAddOnCommands.TryGetValue(e.CycloidalGearAddOnCommand.Session.Identifier,
                    out _))
            {
                _cycloidalGearAddOnCommands.Remove(e.CycloidalGearAddOnCommand.Session.Identifier);
            }
        }

        #endregion

        #region HelpAbout

        private static IAlibreAddOnCommand DoHelpAbout()
        {
            var aboutForm = new AboutForm();
            aboutForm.Visible = true;
            return null;
        }

        #endregion

        #region DataBrowser

        /// <summary>
        /// Opens the DataBrowser.
        /// Note that the DataBrowser returned is a static instance.
        /// Any files already indexed by the DataBrowser will not show updated data if subsequently saved via Alibre. 
        /// </summary>
        /// <returns></returns>
        private static IAlibreAddOnCommand DoDataBrowser()
        {
            var browserForm = DataBrowserForm.Instance();
            browserForm.Visible = true;
            return null;
        }

        #endregion

        #region Bevel
        
        /// <summary>
        /// </summary>
        /// <returns></returns>
        private static IAlibreAddOnCommand DoBevel()
        {
            var form = new BevelGearForm();
            form.Show();

            return null;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        private static IAlibreAddOnCommand DoAdvancedGear()
        {
            var form = new InvoluteGearForm();
            form.Show();

            return null;
        }

        #endregion
        
        #region RackSpur
        
        private  IAlibreAddOnCommand DoRackSpur()
        {
            var form = new RackPinionForm();
            form.Show();
            return null;
        }

     

        #endregion

        #region WormGear

        private  IAlibreAddOnCommand DoWormGear()
        {
            var form = new WormGearForm();
            form.Show();
            return null;
        }

        #endregion

        /// <summary>
        /// Loads Data from AddOn
        /// </summary>
        /// <param name="pCustomData"></param>
        /// <param name="sessionIdentifier"></param>
        public void LoadData(IStream pCustomData, string sessionIdentifier)
        {
        }

        /// <summary>
        /// Saves Data to AddOn
        /// </summary>
        /// <param name="pCustomData"></param>
        /// <param name="sessionIdentifier"></param>
        public void SaveData(IStream pCustomData, string sessionIdentifier)
        {
        }

        /// <summary>
        /// Sets the IsLicensed bit for the tightly coupled Add-on
        /// </summary>
        /// <param name="isLicensed"></param>
        public void setIsAddOnLicensed(bool isLicensed)
        {
        }

        /// <summary>
        /// Returns True if the AddOn needs to use a Dedicated Ribbon Tab
        /// </summary>
        /// <returns></returns>
        public bool UseDedicatedRibbonTab()
        {
            return false;
        }
    }
}