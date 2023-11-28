using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CsvImporter))]
public class CsvImporterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var csvImporter = target as CsvImporter;
        DrawDefaultInspector();

        if (GUILayout.Button("�G�f�[�^�̍쐬"))
        {
            Debug.Log("�G�f�[�^�̍쐬�{�^���������ꂽ");
            SetCsvDataToScriptableObject(csvImporter);
        }
    }

    void SetCsvDataToScriptableObject(CsvImporter csvImporter)
    {
        //�{�^���������ꂽ��o�[�X���s
        if(csvImporter.csvFile == null)
        {
            Debug.LogWarning(csvImporter.name + ":�ǂݍ���CSV�t�@�C�����Z�b�g����Ă��܂���");
            return;
        }

        //CSV�t�@�C����string�`���ɕϊ�
        string csvText = csvImporter.csvFile.text;

        //���s���ƂɃp�[�X
        string[] afterParse = csvText.Split('\n');

        //�w�b�_�[�s�������ăC���|�[�g
        for(int i = 1; i < afterParse.Length; i++)
        {
            string[] parseByComma = afterParse[i].Split(',');

            int column = 0;

            //�擪�̗񂪋�ł���΂��̍s�͓ǂݍ��܂Ȃ�
            if (parseByComma[column] == "")
                continue;

            //�s����ID�Ƃ��ăt�@�C�����쐬
            string fileName = "enemyData_" + i.ToString("D4") + ".asset";
            string path = "Assets/" + fileName;

            //EnemyData�̃C���X�^���X����������ɍ쐬
            var enemyData = CreateInstance<EnemyData>();

            //���O
            enemyData.enemyName = parseByComma[column];

            //�ő�HP
            column += 1;
            enemyData.maxHp = int.Parse(parseByComma[column]);

            //�U����
            column += 1;
            enemyData.atk = int.Parse(parseByComma[column]);

            //�h���
            column += 1;
            enemyData.def = int.Parse(parseByComma[column]);

            //�o���l
            column += 1;
            enemyData.exp = int.Parse(parseByComma[column]);

            //�S�[���h
            column += 1;
            enemyData.gold = int.Parse(parseByComma[column]);

            //�C���X�^���X���������̂��A�Z�b�g�Ƃ��ĕۑ�
            var asset = (EnemyData)AssetDatabase.LoadAssetAtPath(path, typeof(EnemyData));
            if(asset == null)
            {
                //�w��̃p�X�Ƀt�@�C�������݂��Ȃ��ꍇ�͐V�K�쐬
                AssetDatabase.CreateAsset(enemyData, path);
            }
            else
            {
                //�w��̃p�X�Ɋ��ɓ����̃t�@�C�������݂���ꍇ�͍X�V
                EditorUtility.CopySerialized(enemyData, asset);
                AssetDatabase.Refresh();
            }
            AssetDatabase.Refresh();
        }
        Debug.Log(csvImporter.name + ":�G�̃f�[�^�̍쐬���������܂����B");
    }
}
