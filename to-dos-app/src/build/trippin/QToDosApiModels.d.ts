import { QNumberPath, QStringPath, QDateTimeOffsetPath, QId, QNumberParam, QueryObject } from "@odata2ts/odata-query-objects";
import { ToDoId } from "./ToDosApiModelsModel";
export declare class QToDo extends QueryObject {
    readonly id: QNumberPath<number>;
    readonly task: QStringPath<string>;
    readonly createdDateTime: QDateTimeOffsetPath<string>;
    readonly completedDateTime: QDateTimeOffsetPath<string>;
}
export declare const qToDo: QToDo;
export declare class QToDoId extends QId<ToDoId> {
    private readonly params;
    getParams(): QNumberParam<number>[];
}
