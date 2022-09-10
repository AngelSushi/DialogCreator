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

    private VisualElement leftPane, rightPane,upPane,downPane;
    void CreateGUI() {
        
        dialogs = Resources.LoadAll("Dialogs", typeof(Dialog)).Cast<Dialog>().ToArray();
        
        InitViews();

        ListView list = new ListView();

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
        
        Image sprite = new Image();
        sprite.scaleMode = ScaleMode.ScaleToFit;
        sprite.sprite =  Resources.Load<Sprite>("dialogBackground");

        Image authorSprite = new Image();
            //    authorSprite.scaleMode 
        authorSprite.sprite = (selectedDialog.First() as Dialog).authorSprite; 

        downPane.Add(authorSprite);
       // downPane.Add(sprite);
    }
}
