import { Component, OnInit } from '@angular/core';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {  FormGroup, FormControl, FormArray, Validators } from '@angular/forms';
//import { HttpHeaders } from '@angular/common/http';
import { ApiBuilderService } from 'src/app/services/api-builder.service';
import { Note } from 'src/app/models/Note';
import { Label } from 'src/app/models/Label';
import { Observable } from 'rxjs';
import { toDo } from '../models/toDo';

@Component({
  selector: 'app-note',
  templateUrl: './note.component.html',
  styleUrls: ['./note.component.css']
})
export class NoteComponent implements OnInit {
  notes: Array<Note>;
  noteInfo: Note = new Note();
  noteList: any;
  labels: Array<Label>;
  labelInfo: Label = new Label();
  searchString: string ="";
  currentSelectedNote: Note = new Note();
  toDoNoteInfo: Note = new Note();
  toDo: any;
  marked: Boolean = false;
  theCheckbox: Boolean = false;
  loading: Boolean = false;
  submitted: Boolean = false;
  titles: Array<String>;
  constructor(private http: HttpClient, private ApiBuilderService: ApiBuilderService) { 
    this.toDoNoteInfo.ToDoNotes = new Array<toDo>();
    this.toDoNoteInfo.ToDoNotes.push(new toDo());
  }
 

  ngOnInit() {
    this.getAllNotes();
    this.getAllLabels();
  }

  getAllNotes() {
    this.loading = true;
    this.http.get(this.ApiBuilderService.buildURL('note/all?searchString='+this.searchString)).subscribe((data: any) => {
    this.notes = data;
    this.loading = false;
    });
  }

  public updateNote() {
    this.http.put(this.ApiBuilderService.buildURL('note/updateNote?id='+this.currentSelectedNote.Id), this.currentSelectedNote).subscribe((data: any) => {
    //this.noteInfo = data.hasOwnProperty('data') ? data['data'] : [];
    this.getAllNotes();
		});
  }

  public addNote(isValid){
    this.submitted = true;
    if(isValid){
      this.noteInfo.Label = this.noteInfo.Label1.Id;
      this.http.post(this.ApiBuilderService.buildURL('note/add/'), this.noteInfo).subscribe((data: any) => {
       this.submitted = false;
        this.getAllNotes();
        });
    }
  }

  public deleteNote() {
    this.http.delete(this.ApiBuilderService.buildURL('note/delete?id='+this.currentSelectedNote.Id)).subscribe((data: any) => {
    this.getAllNotes();
		});
  }

  getAllLabels() {
    this.http.get(this.ApiBuilderService.buildURL('label/all')).subscribe((data: any) => {
    this.labels = data;
    });
  }

  public searchByTitle(){
    this.http.get(this.ApiBuilderService.buildURL('note/titlelist?searchString='+this.searchString)).subscribe((data: any) => {
      this.titles= data;
      });
  }

  public passNoteToModal(currentSelectedNote){
    this.currentSelectedNote = currentSelectedNote;
  }

  public toggleVisibility(e){
    this.marked= e.target.checked;
  }
  //add to note
  public addNewTodoInList(){
    this.toDoNoteInfo.ToDoNotes.push(new toDo());
  }
  
  public removeTodoFromList(T){
    if (this.toDoNoteInfo.ToDoNotes.length !== 1){
      this.toDoNoteInfo.ToDoNotes.pop();
    }
  }

  public addTodoNote(){
    console.log("add todo button clicked");
    this.toDoNoteInfo.Label = this.toDoNoteInfo.Label1.Id;
    this.http.post(this.ApiBuilderService.buildURL('note/addToDoNote'), this.toDoNoteInfo).subscribe((data: any) => {
      this.getAllNotes();
      });
  }

}
