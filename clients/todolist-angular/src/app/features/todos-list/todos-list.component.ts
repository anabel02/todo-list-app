import {Component, OnInit, signal} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {CommonModule} from '@angular/common';

import {MatCardModule} from '@angular/material/card';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {MatListModule} from '@angular/material/list';
import {MatCheckboxModule} from '@angular/material/checkbox';
import {MatDividerModule} from '@angular/material/divider';
import {MatProgressBarModule} from '@angular/material/progress-bar';
import {CreateTodo, TodoService} from '../../core/services/todos.service';
import {IsNullOrWhitespacePipe} from '../../core/pipes/is-null-or-whitespace.pipe';
import {Todo} from '../../core/models/todo.model';
import {MatButtonToggle, MatButtonToggleChange, MatButtonToggleGroup} from '@angular/material/button-toggle';
import {QueryOptions} from 'odata-query';

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
    MatButtonToggle,
    MatButtonToggleGroup,
  ],
  templateUrl: './todos-list.component.html',
  styleUrls: ['./todos-list.component.scss'],
})
export class TodosListComponent implements OnInit {
  todos = signal<Todo[]>([]);
  loading = signal(false);
  error = signal<string | null>(null);
  newTodoTitle = signal('');

  constructor(private todoService: TodoService) {
  }

  ngOnInit(): void {
    this.fetch();
  }

  trackById(index: number, item: Todo) {
    return item.id;
  }

  filter = signal<'all' | 'active' | 'completed'>('all');

  onFilterChange(event: MatButtonToggleChange) {
    this.filter.set(event.value as 'all' | 'active' | 'completed');
    this.fetch()
  }

  fetch() {
    this.loading.set(true);
    this.error.set(null);

    let queryOptions: Partial<QueryOptions<Todo>> | undefined;

    switch (this.filter()) {
      case 'active':
        queryOptions = {filter: {completedDateTime: null}};
        break;
      case 'completed':
        queryOptions = {filter: {completedDateTime: {ne: null}}}; // `ne` operator is supported
        break;
      default:
        queryOptions = undefined; // fetch all
        break;
    }

    this.todoService.getAll(queryOptions).subscribe({
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

    const body: CreateTodo = {task: title};
    this.todoService.create(body).subscribe({
      next: (todo) => {
        // signal-friendly update
        this.todos.update((list) => [...list, todo]);
        this.newTodoTitle.set('');
      },
    });
  }

  toggleCompleted(todo: Todo) {
    if (!todo.completedDateTime) {
      this.todoService.complete(todo.id).subscribe({
        next: () => {
          this.todos.update((list) =>
            list.map((t) =>
              t.id === todo.id
                ? {...t, completedDateTime: new Date().toISOString()}
                : t
            )
          );
        },
      });
    }
  }

  logout() {
    localStorage.removeItem('access_token');
    localStorage.removeItem('refresh_token');
    window.location.href = '/login';
  }
}
