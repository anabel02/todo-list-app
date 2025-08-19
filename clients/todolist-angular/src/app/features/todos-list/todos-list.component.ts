import {Component, OnInit, signal} from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { MatCardModule } from '@angular/material/card';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDividerModule } from '@angular/material/divider';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import {CreateTodo, Todo, TodoService} from '../../core/services/todos.service';
import {IsNullOrWhitespacePipe} from '../../core/pipes/is-null-or-whitespace.pipe';

@Component({
  selector: 'app-todos',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatCardModule,
    MatToolbarModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatListModule,
    MatCheckboxModule,
    MatDividerModule,
    MatProgressBarModule,
    IsNullOrWhitespacePipe,
  ],
  templateUrl: './todos-list.component.html',
  styleUrls: ['./todos-list.component.scss'],
})
export class TodosListComponent implements OnInit {
  todos = signal<Todo[]>([]);
  loading = signal(false);
  error = signal<string | null>(null);
  newTodoTitle = signal('');

  constructor(private todoService: TodoService) {}

  ngOnInit(): void {
    this.fetch();
  }

  trackById(index: number, item: Todo) {
    return item.id;
  }

  fetch() {
    this.loading.set(true);
    this.error.set(null);

    this.todoService.getTodos().subscribe({
      next: (data) => {
        this.todos.set(data);
        this.loading.set(false);
      },
      error: () => {
        this.error.set('Failed to load todos.');
        this.loading.set(false);
      },
    });
  }

  addTodo() {
    const title = this.newTodoTitle();
    if (!title.trim()) return;

    const body: CreateTodo = { task: title };
    this.todoService.createTodo(body).subscribe({
      next: (todo) => {
        // signal-friendly update
        this.todos.update((list) => [...list, todo]);
        this.newTodoTitle.set('');
      },
    });
  }

  toggleCompleted(todo: Todo) {
    if (!todo.completedDateTime) {
      this.todoService.completeTodo(todo.id).subscribe({
        next: () => {
          this.todos.update((list) =>
            list.map((t) =>
              t.id === todo.id
                ? { ...t, completedDateTime: new Date().toISOString() }
                : t
            )
          );
        },
      });
    }
  }

  updateTitle(todo: Todo) {
    if (todo.completedDateTime) return;
    this.todoService.updateTodo(todo.id, { task: todo.task }).subscribe();
  }

  deleteTodo(todo: Todo) {
    this.todoService.deleteTodo(todo.id).subscribe({
      next: () =>
        this.todos.update((list) => list.filter((t) => t.id !== todo.id)),
    });
  }

  logout() {
    localStorage.removeItem('token');
    window.location.href = '/login';
  }
}
