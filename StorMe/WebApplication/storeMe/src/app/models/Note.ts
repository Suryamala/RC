import { Url } from 'url';
import { Label } from 'src/app/models/Label';
import { toDo } from './toDo';

export class Note {
    // Declared all properties names as in backend as it is mandatory in Angular 7
    Id!: number;
    Title!: string;
    Label!: number;
    Note1!: string;  // Note content
    Label1!: Label;  // Label Id and Name
    ToDoNotes!: Array<toDo>;

    constructor(){
    this.Label1=new Label();
    this.ToDoNotes = new Array<toDo>();
    }
}