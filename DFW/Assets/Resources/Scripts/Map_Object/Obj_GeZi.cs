using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 格子对象
/// 1、“Pos_X”       坐标X
/// 2、“Pos_Y”       坐标Y
/// 3、“icon_direct” 图标方向（“L”-左；"R"-右；"U"-上；"D"-下）
/// 4、“name”        格子名字
/// 5、“Type”        格子类型（0-空地；1-房子；2-）
/// </summary>
public class Obj_GeZi {
    //坐标XY
    public int Pos_X;
    public int Pos_Y;
    //图标位子
    public int Icon_direct;
    //格子名字
    public string Name;
    public int Type;
    public string prefab_path;

    public void init(Dictionary<string, object> tInit) {
        
    }
}
