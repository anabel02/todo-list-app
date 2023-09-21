import { ODataHttpClient } from "@odata2ts/http-client-api";
import { EntityTypeServiceV4, EntitySetServiceV4 } from "@odata2ts/odata-service";
import { ToDo, EditableToDo, ToDoId } from "../ToDosApiModelsModel";
import { QToDo } from "../QToDosApiModels";
export declare class ToDoService<ClientType extends ODataHttpClient> extends EntityTypeServiceV4<ClientType, ToDo, EditableToDo, QToDo> {
    constructor(client: ClientType, basePath: string, name: string);
}
export declare class ToDoCollectionService<ClientType extends ODataHttpClient> extends EntitySetServiceV4<ClientType, ToDo, EditableToDo, QToDo, ToDoId> {
    constructor(client: ClientType, basePath: string, name: string);
}
