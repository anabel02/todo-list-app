import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environment';
import { Observable } from 'rxjs';
import {ODataService} from './odata-base.service';

export interface Todo {
  id: number;
  task: string;
  completedDateTime?: string;
}

export interface CreateTodo {
  task: string;
}

export interface UpdateTodo {
  task: string;
}

@Injectable({ providedIn: 'root' })
export class TodoService extends ODataService<Todo> {
  protected override baseUrl = `${environment.apiBaseUrl}/toDos`;

  constructor(http: HttpClient) {
    super(http);
  }

  createTodo(body: CreateTodo): Observable<Todo> {
    return this.http.post<Todo>(this.baseUrl, body);
  }

  updateTodo(id: number, body: UpdateTodo): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/${id}`, body);
  }

  completeTodo(id: number): Observable<Date> {
    return this.http.put<Date>(`${this.baseUrl}/${id}/complete`, {});
  }

  deleteTodo(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
