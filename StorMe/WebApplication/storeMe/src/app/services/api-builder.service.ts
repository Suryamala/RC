import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ApiBuilderService {
  public buildURL(api:string): string {
    return "http://localhost:1478/api/"+api;
    }
}
