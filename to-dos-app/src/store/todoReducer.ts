import { Todo, TodoAction, TodoState } from "../types/type";
import { ActionType } from "./actions";

const initialState: TodoState = { completedTodos: [], notCompletedTodos: [], activeTodos: [], loading: true };

export const todoReducer = (state: TodoState = initialState, action: TodoAction) => {
    switch (action.type) {
        case ActionType.Add:
            // when you add a new to do, it goes to the not completed todos
            return {
                ...state,
                notCompletedTodos: [...state.notCompletedTodos, action.payload.todo!],
                loading: false
            };

        case ActionType.Remove:
            return {
                ...state,
                completedTodos: state.completedTodos.filter(todo => todo.Id !== action.payload.todo!.Id),
                notCompletedTodos: state.notCompletedTodos.filter(todo => todo.Id !== action.payload.todo!.Id),
                loading: false
            };

        case ActionType.Complete:
            // when you mark a to do as completed, it is delete from not completed and add to completed
            const todo: Todo | undefined = state.notCompletedTodos.find(todo => todo.Id === action.payload.todo!.Id);
            if (todo === undefined) return state;
            return {
                ...state,
                notCompletedTodos: state.notCompletedTodos.filter(todo => todo.Id !== action.payload.todo!.Id),
                completedTodos: [ action.payload.todo!, ...state.completedTodos],
                loading: false
            };

        case ActionType.Edit:
            // you only can update a not completed to do
            return {
                ...state,
                notCompletedTodos: state.notCompletedTodos.map(todo => todo.Id === action.payload.todo!.Id ? {
                    ...todo,
                    task: action.payload.todo!.Task
                }
                    : todo),
                loading: false
            };

        case ActionType.SetTodos:
            return {
                ...state,
                notCompletedTodos: action.payload.notCompletedTodos!,
                completedTodos: action.payload.completedTodos!,
                loading: false
            };

        case ActionType.SetActiveTodos:
            return {
                ...state,
                activeTodos: action.payload.activeTodos!,
                loading: false
            }

        default:
            return state;
    };
};