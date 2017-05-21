using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PersistentDataHolder
{
    // The selected player material
    private static Material selectedPlayerSkin;
	

    /*--Getters and Setters--*/

    public static Material getSelectedPlayerSkin()
    {
        return selectedPlayerSkin;
    }

    public static void setSelectedPlayerSkin(Material newSelectedPlayerSkin)
    {
        selectedPlayerSkin = newSelectedPlayerSkin;
    }

}
