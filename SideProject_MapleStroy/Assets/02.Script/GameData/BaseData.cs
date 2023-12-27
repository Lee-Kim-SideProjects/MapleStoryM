using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// data의 기본 클래스
/// 공통적인 데이터를 가지고 있게 되는데, 현재 이름만 가지고 있다.
/// 데이터의 갯수와 이름의 목록 리스트를 얻을 수 있다.
/// </summary>
/// 

public class BaseData : ScriptableObject // 클래스 인스턴스와는 별도로 대량의 데이터를 저장하는 데 사용할 수 있는 데이터 컨테이너
{
    // 데이터가 저장될 기본 디렉토리 경로를 상수로 정의
    public const string dataDirectory = "/10.ResourcesData/Resources/Data/";

    // 이름을 저장하는 문자열 배열
    public string[] names = null;

    // 기본 생성자
    public BaseData() { }

    // 데이터 개수를 반환하는 함수
    public int GetDataCount()
    {
        int retValue = 0;

        // names 배열이 null이 아닐 때, 배열의 길이를 반환
        if (this.names != null)
        {
            retValue = this.names.Length;
        }

        return retValue;
    }

    /// 이름 목록을 생성하는 함수
    /// showID 파라미터가 true일 경우 ID와 이름을 모두 표시 함
    /// filterWord를 사용해 특정 단어가 포함된 이름만 필터링 할 수 있다.
    public string[] GetNameList(bool showID, string filterWord = "")
    {
        string[] retList = new string[0];

        // names 배열이 null인 경우, 빈 배열을 반환 함
        if (this.names == null)
        {
            return retList;
        }

        retList = new string[this.names.Length];

        // names 배열을 순회하면서 필터링 조건을 만족하는 이름을 retList에 추가
        for (int i = 0; i < this.names.Length; i++)
        {
            // 필터링 조건을 확인
            if (filterWord != "")
            {
                if (names[i].ToLower().Contains(filterWord.ToLower()) == false)
                {
                    continue;
                }
            }
            // ID를 표시할지 여부에 따라 retList에 추가
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

    // 새 데이터를 추가하는 함수 (현재는 구현되지 않음)
    public virtual int AddData(string newName)
    {
        return GetDataCount();
    }

    // 데이터를 제거하는 함수 (현재는 구현되지 않음)
    public virtual void RemoveData(int index)
    {
    }

    // 데이터를 복사하는 함수 (현재는 구현되지 않음)
    public virtual void Copy(int index)
    {
    }
}
