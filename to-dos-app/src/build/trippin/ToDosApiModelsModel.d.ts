export interface ToDo {
    /**
     * **Key Property**: This is a key property used to identify the entity.<br/>**Managed**: This property is managed on the server side and cannot be edited.
     *
     * OData Attributes:
     * |Attribute Name | Attribute Value |
     * | --- | ---|
     * | Name | `Id` |
     * | Type | `Edm.Int32` |
     * | Nullable | `false` |
     */
    id: number;
    /**
     *
     * OData Attributes:
     * |Attribute Name | Attribute Value |
     * | --- | ---|
     * | Name | `Task` |
     * | Type | `Edm.String` |
     * | Nullable | `false` |
     */
    task: string;
    /**
     *
     * OData Attributes:
     * |Attribute Name | Attribute Value |
     * | --- | ---|
     * | Name | `CreatedDateTime` |
     * | Type | `Edm.DateTimeOffset` |
     */
    createdDateTime: string | null;
    /**
     *
     * OData Attributes:
     * |Attribute Name | Attribute Value |
     * | --- | ---|
     * | Name | `CompletedDateTime` |
     * | Type | `Edm.DateTimeOffset` |
     */
    completedDateTime: string | null;
}
export type ToDoId = number | {
    id: number;
};
export interface EditableToDo extends Pick<ToDo, "task">, Partial<Pick<ToDo, "createdDateTime" | "completedDateTime">> {
}
