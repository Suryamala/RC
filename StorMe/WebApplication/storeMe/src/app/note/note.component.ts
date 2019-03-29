import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ApiBuilderService } from 'src/app/services/api-builder.service';
import { Note } from 'src/app/models/Note';
import { Label } from 'src/app/models/Label';
import { toDo } from '../models/toDo';
import { NgForm } from '@angular/forms';

declare var $: any;

@Component({
  selector: 'app-note',
  templateUrl: './note.component.html',
  styleUrls: ['./note.component.css']
})
export class NoteComponent implements OnInit {
  notes: Array<Note>;
  noteInfo: Note = new Note();
  labels: Array<Label>;
  searchString: string = "";
  currentSelectedNote: Note = new Note();
  toDoNoteInfo: Note = new Note();
  marked: Boolean = false;
  loading: Boolean = false;
  addNoteFormSubmitted: Boolean = false;
  addToDoNoteFormSubmitted: Boolean = false;
  updateNoteFormSubmitted:Boolean = false;
  titles: Array<String>;
  selectedLabelId: number = 0;
  selectedAddLabelId: number = 0;
  emptyToDoFound: Boolean= false;
  constructor(private http: HttpClient, private ApiBuilderService: ApiBuilderService) {
    // To declare empty list for To Do items and push one empty item by default
    this.toDoNoteInfo.ToDoNotes = new Array<toDo>();
    this.toDoNoteInfo.ToDoNotes.push(new toDo());
  }


  ngOnInit() {
    // To load all notes and labels at the start of application
    this.getAllNotes();
    this.getAllLabels();
  }

  // Get all notes list(Search string can be provided)
  getAllNotes() {
    this.loading = true;
    this.http.get(this.ApiBuilderService.buildURL('note/all?searchString=' + this.searchString)).subscribe((data: any) => {
      this.notes = data;
      this.loading = false;
    });
  }

  // Update the note by Id(Note can be simple note or To Do note)
  public updateNote(isValid) {
    this.updateNoteFormSubmitted = true;
    if(isValid){
      this.http.put(this.ApiBuilderService.buildURL('note/updateNote?id=' + this.currentSelectedNote.Id), this.currentSelectedNote).subscribe((data: any) => {
        this.getAllNotes();
      });
      this.updateNoteFormSubmitted = false;
      $('#updateNoteModal').modal('toggle');
   }
  }

  // Add new simple note
  public addNote(form:NgForm) {
    this.addNoteFormSubmitted = true;
    if(form.valid && this.selectedAddLabelId) {
      this.noteInfo.Label = this.selectedAddLabelId;
      this.http.post(this.ApiBuilderService.buildURL('note/add/'), this.noteInfo).subscribe((data: any) => {
        this.getAllNotes();
      });
      this.addNoteFormSubmitted = false;
      $('#addNoteModal').modal('toggle');
      form.reset();
    }
  }

  // Delete note by Id
  public deleteNote() {
    this.http.delete(this.ApiBuilderService.buildURL('note/delete?id=' + this.currentSelectedNote.Id)).subscribe((data: any) => {
      this.getAllNotes();
    });
  }

  // Add To Do note
  public addTodoNote(form:NgForm) {
    this.addToDoNoteFormSubmitted = true;
    this.emptyToDoFound = false;
    for(var i=0; i<this.toDoNoteInfo.ToDoNotes.length; i++){
      if(this.toDoNoteInfo.ToDoNotes[i].toDoName == null){
        this.emptyToDoFound = true;
      }
      else{
        this.emptyToDoFound = false;
      }
    }
    if(form.valid && this.selectedLabelId){
      this.toDoNoteInfo.Label = this.selectedLabelId;
      this.http.post(this.ApiBuilderService.buildURL('note/addToDoNote'), this.toDoNoteInfo).subscribe((data: any) => {
        this.getAllNotes();
      });
      this.addToDoNoteFormSubmitted = false;
      $('#addTodoModal').modal('toggle');
      form.reset();
    }
  }

  // Get all labels list
  getAllLabels() {
    this.http.get(this.ApiBuilderService.buildURL('label/all')).subscribe((data: any) => {
      this.labels = data;
    });
  }

  // Get Title list by search string for lookahead search
  public searchByTitle() {
    this.http.get(this.ApiBuilderService.buildURL('note/titlelist?searchString=' + this.searchString)).subscribe((data: any) => {
      this.titles = data;
    });
  }

  // To pass selected note data to update note modal form
  public passNoteToModal(currentSelectedNote) {
    this.updateNoteFormSubmitted = false;
    this.currentSelectedNote = currentSelectedNote;
  }

  // Add new To Do item in a To Do list while adding To Do note 
  public addNewTodoInList() {
    let todo = new toDo();
    this.toDoNoteInfo.ToDoNotes.push(todo);
  }

  // Remove new To Do item in a To Do list while adding To Do note(at-least one item is mandatory)
  public removeTodoFromList(T) {
    if (this.toDoNoteInfo.ToDoNotes.length !== 1) {
      this.toDoNoteInfo.ToDoNotes.pop();
    }
  }

  // Reset Update note form after clicking cancel
  public resetUpdatedNote(form:NgForm) {
    $('#updateNoteModal').modal('toggle');
    this.updateNoteFormSubmitted = false;
    this.getAllNotes();
  }

  // Reset Add note form after clicking cancel
  public resetAddedNote(form:NgForm) {
    $('#addNoteModal').modal('toggle');
    this.addNoteFormSubmitted = false;
    form.reset();
  }

  // Reset Add To Do note form after clicking cancel
  public resetAddedToDoNote(form:NgForm) {
    $('#addTodoModal').modal('toggle');
    this.addToDoNoteFormSubmitted = false;
    form.reset();
  }

}
