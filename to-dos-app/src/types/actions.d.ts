enum ActionType {
    Add,
    Delete,
    Toggle,
    Update
}

interface Action {
    type: ActionType,
    payload: Todo
}