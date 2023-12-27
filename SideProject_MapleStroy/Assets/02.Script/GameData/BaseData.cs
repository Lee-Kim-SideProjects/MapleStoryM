using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// data�� �⺻ Ŭ����
/// �������� �����͸� ������ �ְ� �Ǵµ�, ���� �̸��� ������ �ִ�.
/// �������� ������ �̸��� ��� ����Ʈ�� ���� �� �ִ�.
/// </summary>
/// 

public class BaseData : ScriptableObject // Ŭ���� �ν��Ͻ��ʹ� ������ �뷮�� �����͸� �����ϴ� �� ����� �� �ִ� ������ �����̳�
{
    // �����Ͱ� ����� �⺻ ���丮 ��θ� ����� ����
    public const string dataDirectory = "/10.ResourcesData/Resources/Data/";

    // �̸��� �����ϴ� ���ڿ� �迭
    public string[] names = null;

    // �⺻ ������
    public BaseData() { }

    // ������ ������ ��ȯ�ϴ� �Լ�
    public int GetDataCount()
    {
        int retValue = 0;

        // names �迭�� null�� �ƴ� ��, �迭�� ���̸� ��ȯ
        if (this.names != null)
        {
            retValue = this.names.Length;
        }

        return retValue;
    }

    /// �̸� ����� �����ϴ� �Լ�
    /// showID �Ķ���Ͱ� true�� ��� ID�� �̸��� ��� ǥ�� ��
    /// filterWord�� ����� Ư�� �ܾ ���Ե� �̸��� ���͸� �� �� �ִ�.
    public string[] GetNameList(bool showID, string filterWord = "")
    {
        string[] retList = new string[0];

        // names �迭�� null�� ���, �� �迭�� ��ȯ ��
        if (this.names == null)
        {
            return retList;
        }

        retList = new string[this.names.Length];

        // names �迭�� ��ȸ�ϸ鼭 ���͸� ������ �����ϴ� �̸��� retList�� �߰�
        for (int i = 0; i < this.names.Length; i++)
        {
            // ���͸� ������ Ȯ��
            if (filterWord != "")
            {
                if (names[i].ToLower().Contains(filterWord.ToLower()) == false)
                {
                    continue;
                }
            }
            // ID�� ǥ������ ���ο� ���� retList�� �߰�
            if (showID)
            {
                retList[i] = i.ToString() + " : " + this.names[i];
            }
            else
            {
                retList[i] = this.names[i];
            }
        }

        return retList;
    }

    // �� �����͸� �߰��ϴ� �Լ� (����� �������� ����)
    public virtual int AddData(string newName)
    {
        return GetDataCount();
    }

    // �����͸� �����ϴ� �Լ� (����� �������� ����)
    public virtual void RemoveData(int index)
    {
    }

    // �����͸� �����ϴ� �Լ� (����� �������� ����)
    public virtual void Copy(int index)
    {
    }
}
