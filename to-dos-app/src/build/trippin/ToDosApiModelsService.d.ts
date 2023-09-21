import { ODataHttpClient } from "@odata2ts/http-client-api";
import { ODataService } from "@odata2ts/odata-service";
import { ToDoId } from "./ToDosApiModelsModel";
import { ToDoCollectionService, ToDoService } from "./service/ToDoService";
export declare class ToDosApiModelsService<ClientType extends ODataHttpClient> extends ODataService<ClientType> {
    toDos(): ToDoCollectionService<ClientType>;
    toDos(id: ToDoId): ToDoService<ClientType>;
}
