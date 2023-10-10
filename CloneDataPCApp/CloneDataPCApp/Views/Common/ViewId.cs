using CloneDataPCApp.Resources;
using System;
using System.Collections.Generic;

namespace CloneDataPCApp.Views
{
    public class ViewId
    {
        public static Dictionary<Type, string> Titles = new Dictionary<Type, string>
        {
            {typeof(HomeView), AppResources.HomeViewTitle},
            {typeof(AlertDialogView), AppResources.DialogAlertTitle},
        };

        public static Dictionary<Type, string> HeaderTitles = new Dictionary<Type, string>
        {
            {typeof(HomeView), "This is Home"}
        };

        public static Dictionary<Type, string> HeaderDescription = new Dictionary<Type, string>
        {
            {typeof(HomeView), "Home is beautiful"}
        };

        public static List<Type> Dialogs = new List<Type>
        {
            typeof(AlertDialogView)
        };
    }
}