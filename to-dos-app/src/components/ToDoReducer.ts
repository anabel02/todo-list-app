export const todoReducer = (state: Todo[] = [], action : Action) => {
    switch (action.type) {
        case ActionType.Add:
            return [... state, action.payload];

        case ActionType.Delete:
            return state.filter( todo => todo.id !== action.payload.id);

        case ActionType.Toggle:
            return state.map( todo => todo.id === action.payload.id ? {
                ...todo, 
                done: !todo.done
            } 
            : todo);

        case ActionType.Update:
            return state.map( todo => todo.id === action.payload.id ? {
                ...todo, 
                task: action.payload.task
            } 
            : todo);
            
        default:
            return state;
    }
}