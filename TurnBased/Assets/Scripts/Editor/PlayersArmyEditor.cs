using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayersArmyEditor : EditorWindow
{
    private static List<PlayerUnitModel> m_playerOneArmy;
    private static List<PlayerUnitModel> m_playerTwoArmy;

    private static List<VisualElement> m_playerOneList;
    private static List<VisualElement> m_playerTwoList;



    [MenuItem("Editor/Players Army Editor Tool")]
    public static void ShowEditor()
    {
        FindAndLoadAsset();
        m_playerOneList = new List<VisualElement>();
        m_playerTwoList = new List<VisualElement>();

        EditorWindow window = GetWindow<PlayersArmyEditor>();
        window.titleContent = new GUIContent("Players Army Editor Tool");
    }

    public void CreateGUI()
    {
        var scrollBarPlayerOne = new ScrollView();
        var scrollBarPlayerTwo = new ScrollView();
        var playerOneVisualElement = new VisualElement();
        var playerTwoVisualElement = new VisualElement();
        playerOneVisualElement.Add(new Label("Player one Army"));
        playerOneVisualElement.Add(scrollBarPlayerOne);
        ShowUnits(m_playerOneList, scrollBarPlayerOne, m_playerOneArmy);
        ShowUnits(m_playerTwoList, scrollBarPlayerTwo, m_playerTwoArmy);
        CreateButton(playerOneVisualElement, "Add", "Add", Align.Center, 100, 20, () => CreateUnitCard(m_playerOneList, scrollBarPlayerOne));
        CreateButton(playerOneVisualElement, "RemoveLast", "RemoveLast", Align.Center, 100, 20, () => RemoveLastCard(scrollBarPlayerOne, m_playerOneList));
        playerTwoVisualElement.Add(new Label("Player two Army"));
        playerTwoVisualElement.Add(scrollBarPlayerTwo);
        CreateButton(playerTwoVisualElement, "Add", "Add", Align.Center, 100, 20, () => CreateUnitCard(m_playerTwoList, scrollBarPlayerTwo));
        CreateButton(playerTwoVisualElement, "RemoveLast", "RemoveLast", Align.Center, 100, 20, () => RemoveLastCard(scrollBarPlayerTwo, m_playerTwoList));
    

        rootVisualElement.Add(playerOneVisualElement);
        rootVisualElement.Add(playerTwoVisualElement);

        var spacer = new ToolbarSpacer();
        spacer.style.height = 35;
        rootVisualElement.Add(spacer);
        CreateButton(rootVisualElement, "Save", "Save", Align.Center, 350, 20, () => SaveUnits());
    }

    private void RemoveLastCard(VisualElement mainRoot, List<VisualElement> playerList)
    {
        var root = playerList.LastOrDefault();
        playerList.Remove(root);
        mainRoot.Remove(root);
        root.Clear();

    }

    private void CreateUnitCard(List<VisualElement> cardList, VisualElement root, string unitName = "Unit name", int health = 0, int attack = 0, int actionPoints = 0, GameObject prefab = null)
    {
        var card = new VisualElement();
        var spacer = new ToolbarSpacer();
        spacer.style.height = 25;
        card.Add(spacer);
        card.Add(new TextField("Unit name") { value = unitName });
        card.Add(new IntegerField("Health") { value = health });
        card.Add(new IntegerField("Attack"){ value = attack });
        card.Add(new IntegerField("Action Points"){ value = actionPoints });
        card.Add(new ObjectField("Unit Prefab"){ value = prefab });
        card.Add(spacer);
        cardList.Add(card);
        root.Add(card);
    }

    private void CreateButton(VisualElement root, string elementName, string text, Align alignment, int width, int height, Action onClick)
    {
        var button = new Button()
        {
            name = elementName,
        };
        button.style.alignSelf = alignment;
        button.style.height = height;
        button.style.width = width;
        button.text = text;
        button.clicked += onClick;
        root.Add(button);
    }

    private static void FindAndLoadAsset()
    {
        var configGUID = AssetDatabase.FindAssets("ArmyConfigurator");
        var objectsPath = AssetDatabase.GUIDToAssetPath(configGUID[0]);
        var config = (ArmyConfiguration) AssetDatabase.LoadAssetAtPath(objectsPath, typeof(ArmyConfiguration));
        m_playerOneArmy = config.playerOneArmy;
        m_playerTwoArmy = config.playerTwoArmy;
    }

    private void ShowUnits(List<VisualElement> cardList, VisualElement root, List<PlayerUnitModel> playerUnits)
    {
        foreach(var unit in playerUnits)
        {
            CreateUnitCard(cardList, root, unit.UnitName, unit.Health, unit.Attack, unit.actionPoints, unit.prefab);
        }
    }

    private void SaveUnits()
    {
        m_playerOneArmy.Clear();
        m_playerTwoArmy.Clear();

        foreach(var e in m_playerOneList)
        {
            var result = new PlayerUnitModel
            {
                UnitName = ((TextField)e[0]).value,
                Health = ((IntegerField)e[1]).value,
                Attack = ((IntegerField)e[2]).value,
                actionPoints = ((IntegerField)e[3]).value,
                prefab = ((ObjectField)e[4]).value as GameObject
            };
            m_playerOneArmy.Add(result);
        }
        foreach(var e in m_playerTwoList)
        {
            var result = new PlayerUnitModel
            {
                UnitName = ((TextField)e[0]).value,
                Health = ((IntegerField)e[1]).value,
                Attack = ((IntegerField)e[2]).value,
                actionPoints = ((IntegerField)e[3]).value,
                prefab = ((ObjectField)e[4]).value as GameObject
            };
            m_playerTwoArmy.Add(result);
        }
        var configGUID = AssetDatabase.FindAssets("ArmyConfigurator");
        var objectsPath = AssetDatabase.GUIDToAssetPath(configGUID[0]);
        var asset = AssetDatabase.LoadAssetAtPath(objectsPath, typeof(ArmyConfiguration));
        AssetDatabase.SaveAssetIfDirty(asset);
    }
}
