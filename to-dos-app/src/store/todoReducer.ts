import { stat } from "fs";
import { Todo, TodoAction, TodoState } from "../types/type";
import { ActionType } from "./actions";

const initialState : TodoState = {completedTodos: [], notCompletedTodos: [], activeTodos: []};

export const todoReducer = (state: TodoState = initialState, action : TodoAction) => {
    switch (action.type) {
        case ActionType.Add :
            // when you add a new to do, it goes to the not completed todos
            return {
                ...state,
                notCompletedTodos: [...state.notCompletedTodos, action.payload.todo!]
            };

        case ActionType.Remove:
            return {
                ...state,
                completedTodos: state.completedTodos.filter(todo => todo.id !== action.payload.todo!.id),
                notCompletedTodos: state.notCompletedTodos.filter(todo => todo.id !== action.payload.todo!.id)
            };

        case ActionType.Complete:
            // when you mark a to do as completed, it is delete from not completed and add to completed
            const todo : Todo | undefined = state.notCompletedTodos.find(todo => todo.id === action.payload.todo!.id);
            if (todo === undefined) return state;
            return {
                ...state,
                notCompletedTodos: state.notCompletedTodos.filter(todo => todo.id === action.payload.todo!.id),
                completedTodos: [{ ...todo,  completedDateTime: todo.completedDateTime}, ...state.completedTodos]
            };

        case ActionType.Edit:
            // you only can update a not completed to do
            return {
                ...state,
                notCompletedTodos: state.notCompletedTodos.map(todo => todo.id === action.payload.todo!.id ? {
                    ...todo, 
                    task: action.payload.todo!.task
                } 
                : todo)
            };

        case ActionType.SetTodos:
            return {
                ...state,
                notCompletedTodos: action.payload.notCompletedTodos!,
                completedTodos: action.payload.completedTodos!
            };

        case ActionType.SetActiveTodos:
            return {
                ...state,
                activeTodos: action.payload.activeTodos!
            }

        default:
            return state;
    };
};