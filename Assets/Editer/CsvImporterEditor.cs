using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CsvImporter))]
public class CsvImporterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var csvImporter = target as CsvImporter;
        DrawDefaultInspector();

        if (GUILayout.Button("敵データの作成"))
        {
            Debug.Log("敵データの作成ボタンが押された");
            SetCsvDataToScriptableObject(csvImporter);
        }
    }

    void SetCsvDataToScriptableObject(CsvImporter csvImporter)
    {
        //ボタンを押されたらバース実行
        if(csvImporter.csvFile == null)
        {
            Debug.LogWarning(csvImporter.name + ":読み込むCSVファイルがセットされていません");
            return;
        }

        //CSVファイルをstring形式に変換
        string csvText = csvImporter.csvFile.text;

        //改行ごとにパース
        string[] afterParse = csvText.Split('\n');

        //ヘッダー行を除いてインポート
        for(int i = 1; i < afterParse.Length; i++)
        {
            string[] parseByComma = afterParse[i].Split(',');

            int column = 0;

            //先頭の列が空であればその行は読み込まない
            if (parseByComma[column] == "")
                continue;

            //行数をIDとしてファイルを作成
            string fileName = "enemyData_" + i.ToString("D4") + ".asset";
            string path = "Assets/" + fileName;

            //EnemyDataのインスタンスをメモリ上に作成
            var enemyData = CreateInstance<EnemyData>();

            //名前
            enemyData.enemyName = parseByComma[column];

            //最大HP
            column += 1;
            enemyData.maxHp = int.Parse(parseByComma[column]);

            //攻撃力
            column += 1;
            enemyData.atk = int.Parse(parseByComma[column]);

            //防御力
            column += 1;
            enemyData.def = int.Parse(parseByComma[column]);

            //経験値
            column += 1;
            enemyData.exp = int.Parse(parseByComma[column]);

            //ゴールド
            column += 1;
            enemyData.gold = int.Parse(parseByComma[column]);

            //インスタンス化したものをアセットとして保存
            var asset = (EnemyData)AssetDatabase.LoadAssetAtPath(path, typeof(EnemyData));
            if(asset == null)
            {
                //指定のパスにファイルが存在しない場合は新規作成
                AssetDatabase.CreateAsset(enemyData, path);
            }
            else
            {
                //指定のパスに既に同名のファイルが存在する場合は更新
                EditorUtility.CopySerialized(enemyData, asset);
                AssetDatabase.Refresh();
            }
            AssetDatabase.Refresh();
        }
        Debug.Log(csvImporter.name + ":敵のデータの作成が完了しました。");
    }
}
