using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System;

public class LoadXMLData : MonoBehaviour 
{

	public TextAsset _pcCombatData;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void LoadPartyCombatData()
	{
		XmlDocument xmlDoc = new XmlDocument(); // xmlDoc is the new xml document.
		xmlDoc.LoadXml(_pcCombatData.text); // load the file.
		XmlNodeList characterList = xmlDoc.GetElementsByTagName("Character"); // array of the level nodes.

		CharacterInfo[] characterArray = new CharacterInfo[3];
		int charCounter = 0;
		//Debug.Log(characterList.Count);
		foreach(XmlNode characterInfo in characterList)
		{
			//create a temp character and a temp pathlist to be stored into the temp character
			CharacterInfo tempChar = new CharacterInfo();

			//set default modifier values
			tempChar._moveSpeedModifier = 1.0f;

			XmlNodeList characterContent = characterInfo.ChildNodes;
			//Debug.Log(characterContent.Count);
			foreach (XmlNode charItem in characterContent)
			{
				if(charItem.Name == "Title")
				{	
					Debug.Log(charItem.Name + " :: " + charItem.InnerText);
					tempChar._title = charItem.InnerText;
				}
				else if(charItem.Name == "Name")
				{
					Debug.Log(charItem.Name + " :: " + charItem.InnerText);
					tempChar._name = charItem.InnerText;
				}
				else if(charItem.Name == "MoveSpeed")
				{
					Debug.Log(charItem.Name + " :: " + charItem.InnerText);
					tempChar._moveSpeed = ConvertStringToInt(charItem.InnerText);
				}
				else if(charItem.Name == "JumpSpeed")
				{
					Debug.Log(charItem.Name + " :: " + charItem.InnerText);
					tempChar._jumpSpeed = ConvertStringToInt(charItem.InnerText);
				}
				//when it gets to the combat trees, first make a list of combat trees, each combat tree has a name and a list of nodes
				else if(charItem.Name == "CombatPaths")
				{
					Debug.Log(charItem.Name);
					List<CombatPath> tempPathList = new List<CombatPath>();
					XmlNodeList combatPaths = charItem.ChildNodes;
					foreach(XmlNode combatPath in combatPaths)
					{
						//create a temp path to store all the information into
						CombatPath tempPath = new CombatPath();
						XmlNodeList pathAttrs = combatPath.ChildNodes;//make a list of path attributes
						foreach(XmlNode pathAttr in pathAttrs)
						{
							if(pathAttr.Name == "Name")
							{
								Debug.Log(pathAttr.Name + " :: " + pathAttr.InnerText);
								tempPath._pathName = pathAttr.InnerText;
							}
							//when the combat tree encounters its nodes, first make a list of them and then start adding them into the temp tree
							else if(pathAttr.Name == "Nodes")
							{
								BinaryTree<ComboNode> tempTree = new BinaryTree<ComboNode>();
								XmlNodeList combatTreeList = pathAttr.ChildNodes; //gets the list of nodes
							
								foreach(XmlNode node in combatTreeList)
								{
									//within each node, is a list of node attributes which are stored into a temp node
									//calculate the insert value of the node from the ID
									XmlNodeList nodeAttributes = node.ChildNodes; //gets the list of the nodes attributes
									ComboNode tempNode = new ComboNode();
									foreach(XmlNode attr in nodeAttributes)
									{
										if(attr.Name == "ID")
										{
											tempNode._id = attr.InnerText;
											char[] charArr = attr.InnerText.ToCharArray();
											float weight = 0.0f;
											float weightMod = 0.1f;

											for(int i = 3; i < charArr.Length; i++)
											{
												if(i == 3)
												{
													weight = 0.0f;
												}
												else
												{
													if(charArr[i] == 'a')
													{
														weight += weightMod;
														weightMod /= 10.0f;
													}
													else if(charArr[i] == 'p')
													{
														weight -= weightMod;
														weightMod /= 10.0f;
													}
												}
											}
											Debug.Log("ID: " + attr.InnerText + " weight: " + weight);
											tempNode._weightValue = weight;
										}
										else if(attr.Name == "Damage")
										{
											tempNode._damage = ConvertStringToInt(attr.InnerText);
											Debug.Log("Attack Damage: " + tempNode._damage);
										}
										else if(attr.Name == "Duration")
										{
											tempNode._attackDuration = ConvertStringToFloat(attr.InnerText);
											Debug.Log("Attack Duration: " + tempNode._attackDuration);
										}
										else if(attr.Name == "Movements")
										{
											XmlNodeList movements = attr.ChildNodes; //gets the list of movements
											AttackMovement[] atkmovArr = new AttackMovement[movements.Count];
											int counter = 0;
											foreach(XmlNode movement in movements)
											{
												AttackMovement tempMov = new AttackMovement();
												XmlNodeList moveAttrs = movement.ChildNodes; //gets the list of the nodes attributes
												foreach(XmlNode moveAttr in moveAttrs)
												{
													if(moveAttr.Name == "Speed")
													{
														tempMov.speed = ConvertStringToFloat(moveAttr.InnerText);
														Debug.Log(moveAttr.Name + " " + tempMov.speed);
													}
													else if(moveAttr.Name == "Duration")
													{
														tempMov.duration = ConvertStringToFloat(moveAttr.InnerText);
														Debug.Log(moveAttr.Name + " " + tempMov.duration);
													}
												}
												atkmovArr[counter] = tempMov;
												counter++;
											}
											tempNode._attackMovements = atkmovArr;
										}
									}
									//once all the attributes are added into the temp node, add the temp node to the temp tree
									//TODO: add tempnode to the tree here!
									
								}
								//then store the temp tree into the combatPath's combat tree
								tempPath._combatTree = tempTree;
							}
						}
						tempPathList.Add(tempPath);
					}
				}

			}
			characterArray[charCounter] = tempChar;
			charCounter++;
		}

		GameState._party = characterArray;
	}

	int ConvertStringToInt(string input)
	{
		int output = -9999;
		try
		{
			output = Convert.ToInt32(input);
		}
		catch (FormatException e)
		{
			Console.WriteLine("Input string is not a sequence of digits.");
		}
		catch (OverflowException e)
		{
			Console.WriteLine("The number cannot fit in an Int32.");
		}

		return output;
	}

	float ConvertStringToFloat(string input)
	{
		float output = -9999;
		try
		{
			output = float.Parse(input);
		}
		catch (OverflowException e)
		{
			Console.WriteLine("The number cannot fit in a float.");
		}
		return output;
	}
}
