using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogCreatorWindow : EditorWindow {

    [MenuItem("AngelSushi/DialogCreator")]
    public static void ShowWindow() {
        GetWindow<DialogCreatorWindow>("DialogCreator");
    }

    private Dialog[] dialogs;
    private ListView list;

    private VisualElement leftPane, rightPane,upPane,downPane;

    private bool selectionChange;
    private Dialog targetDialog;
    
    void CreateGUI() {
        
        dialogs = Resources.LoadAll("Dialogs", typeof(Dialog)).Cast<Dialog>().ToArray();
       

        InitViews();

        list = new ListView();

        list.makeItem = () => new Label();
        list.bindItem = (item, index) =>
        {
            Label targetLabel = (Label)item;
            targetLabel.text = dialogs[index].name;
        };
        list.itemsSource = dialogs;
       
       
        leftPane.Add(list);
       


        list.onSelectionChange += OnDialogSelectionChange;

    }

    private void OnGUI()
    {
        if (selectionChange)
        {
            //if(targetDialog.authorSprite != null)
              //  GUILayout.Label(targetDialog.authorSprite,GUILayout.Width(200),GUILayout.Height(200));
        }
        //GUILayout.Label(Resources.Load<Texture>("Characters/character01"));
    }

    private void InitViews() {
        TwoPaneSplitView splitView = new TwoPaneSplitView(0, 250, TwoPaneSplitViewOrientation.Horizontal);
        rootVisualElement.Add(splitView);

        leftPane = new VisualElement();
        splitView.Add(leftPane);
        rightPane = new VisualElement();
        splitView.Add(rightPane);
       
        TwoPaneSplitView splitRightView = new TwoPaneSplitView(0, 250, TwoPaneSplitViewOrientation.Vertical);
        rightPane.Add(splitRightView);
       
        upPane = new VisualElement();
        splitRightView.Add(upPane);
        downPane = new VisualElement();
        splitRightView.Add(downPane);
    }

    private void OnDialogSelectionChange(IEnumerable<object> selectedDialog) {
        downPane.Clear();
        upPane.Clear();

        Dialog targetDialog = (selectedDialog.First() as Dialog);
        Image sprite = new Image();
        sprite.scaleMode = ScaleMode.ScaleToFit;
        sprite.sprite =  Resources.Load<Sprite>("dialogBackground");

        Image authorSprite = new Image();
        authorSprite.sprite =  (selectedDialog.First() as Dialog).authorSprite;

        Label labelName = new Label();
        labelName.text = "Nom: ";

        TextField fieldName = new TextField();
        fieldName.value = (selectedDialog.First() as Dialog).name;

        Label labelAuthor = new Label();
        labelAuthor.text = "Auteur: ";

        TextField fieldAuthor = new TextField();
        fieldAuthor.value = (selectedDialog.First() as Dialog).author;
        
        Label labelContent = new Label();
        labelContent.text = "Pages: ";

        TextField fieldContent = new TextField();
        string content = "";
        
        for (int i = 0; i < targetDialog.pages.Count; i++)
            content += targetDialog.pages[i] + " , ";

        Label labelRepeatable = new Label();
        labelRepeatable.text = "Repeatable: ";
        
        Toggle repeatable = new Toggle();
        repeatable.value = targetDialog.isRepeatable;

        GameObject obj = new GameObject();
        
        
        fieldContent.value = content;
        
        upPane.Add(labelName);
        upPane.Add(fieldName);
        
        upPane.Add(labelAuthor);
        upPane.Add(fieldAuthor);
        
        upPane.Add(labelContent);
        upPane.Add(fieldContent);
        
        upPane.Add(labelRepeatable);
        upPane.Add(repeatable);
        
        
        
       // upPane.Add(obj);
        
        
        
        downPane.Add(authorSprite);
        downPane.Add(sprite);
    }
}
