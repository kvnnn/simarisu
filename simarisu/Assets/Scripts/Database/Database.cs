using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Database : SingletonMonoBehaviour<Database>
{
	private SqliteDatabase db;

	protected override void Awake()
	{
		base.Awake();
		db = new SqliteDatabase("database.db");
	}

	public DataTable Execute(string query)
	{
		return db.ExecuteQuery(query);
	}
}
