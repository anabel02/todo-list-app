export const todoReducer = (state: Todo[] = [], action : Action) => {
    switch (action.type) {
        case ActionType.Add:
            return [... state, action.payload];

        case ActionType.Delete:
            return state.filter( todo => todo.id !== action.payload);

        case ActionType.Toggle:
            return state.map( todo => todo.id === action.payload ? {
                ...todo, 
                done: !todo.done
            } 
            : todo);
        
        default:
            return state;
    }
}