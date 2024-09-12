using UnityEngine;
using UnityEditor;
 
public class BoundsTool
{
    #if UNITY_EDITOR
    [MenuItem("EditorTool/AutoBoxCollider")]
    private static void AutoBoxCollider()
    {
        //如果未选中任何物体 返回
        GameObject gameObject = Selection.activeGameObject;
        if (gameObject == null) return;
        //计算中心点
        Vector3 center = Vector3.zero;
        var renders = gameObject.GetComponentsInChildren<Renderer>();
        for (int i = 0; i < renders.Length; i++)
        {
            center += renders[i].bounds.center;
        }
        center /= renders.Length;
        //创建边界盒
        Bounds bounds = new Bounds(center, Vector3.zero);
        foreach (var render in renders)
        { 
            bounds.Encapsulate(render.bounds);
        }
        //先判断当前是否有碰撞器 进行销毁
        var currentCollider = gameObject.GetComponent<Collider>();
        if (currentCollider != null) Object.DestroyImmediate(currentCollider); 
        //添加BoxCollider 设置中心点及大小
        var boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.center = bounds.center - gameObject.transform.position;
        boxCollider.size = bounds.size;
    }
    #endif
}