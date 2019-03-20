import { Component, OnInit } from '@angular/core';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormControl, Validators } from '@angular/forms';
//import { HttpHeaders } from '@angular/common/http';
import { ApiBuilderService } from 'src/app/services/api-builder.service';
import { Note } from 'src/app/models/Note';
import { Label } from 'src/app/models/Label';
import { Observable } from 'rxjs';

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
  searchText: any ;
  constructor(private http: HttpClient, private ApiBuilderService: ApiBuilderService) { }

  ngOnInit() {
    this.getAllNotes();
    this.getNoteById(5);
    this.noteInfo.Title =="jgjg";
  }

  getAllNotes() {
    this.http.get(this.ApiBuilderService.buildURL('note')).subscribe((data: any) => {
    this.notes = data;
    });
  }

  getNoteById(Id) {
    this.http.get(this.ApiBuilderService.buildURL('note/'+Id)).subscribe((data: any) => {
  //	this.noteInfo = data.hasOwnProperty('data') ? data['data'] : [];
  console.log("got note data");
		});
  }

  public updateNote(noteInfo) {
    this.http.post(this.ApiBuilderService.buildURL('note/'), this.noteInfo).subscribe((data: any) => {
		this.noteInfo = data.hasOwnProperty('data') ? data['data'] : [];
		});
  }

  public addNote(){
    this.http.put(this.ApiBuilderService.buildURL('note/'), this.noteInfo).subscribe((data: any) => {
    //this.noteInfo = data.hasOwnProperty('data') ? data['data'] : [];
    console.log("add button clicked");
      });
  }

  public addTodoNote(){
    console.log("add todo button clicked");
  }

  getAllLabels() {
    this.http.get(this.ApiBuilderService.buildURL('label')).subscribe((data: any) => {
    this.labels = data;
    });
  }

  public addLabel(labelInfo){
    this.http.put(this.ApiBuilderService.buildURL('label/'), this.labelInfo).subscribe((data: any) => {
      console.log("add new label");
        });
  }

  public searchByTitle(){
    this.http.get(this.ApiBuilderService.buildURL('note'), this.searchText).subscribe((data: any) => {
      this.notes = data;
      });
  }

  public searchByTitleAndLabel(){
    if(this.searchText){
      this.http.get(this.ApiBuilderService.buildURL('note'), this.searchText).subscribe((data: any) => {
        this.notes = data;
    });
      }
  }

}
