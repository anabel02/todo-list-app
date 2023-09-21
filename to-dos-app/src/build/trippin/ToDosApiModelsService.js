var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (Object.prototype.hasOwnProperty.call(b, p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        if (typeof b !== "function" && b !== null)
            throw new TypeError("Class extends value " + String(b) + " is not a constructor or null");
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
import { ODataService } from "@odata2ts/odata-service";
import { QToDoId } from "./QToDosApiModels";
import { ToDoCollectionService, ToDoService } from "./service/ToDoService";
var ToDosApiModelsService = /** @class */ (function (_super) {
    __extends(ToDosApiModelsService, _super);
    function ToDosApiModelsService() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    ToDosApiModelsService.prototype.toDos = function (id) {
        var fieldName = "ToDos";
        var _a = this.__base, client = _a.client, path = _a.path;
        return typeof id === "undefined" || id === null
            ? new ToDoCollectionService(client, path, fieldName)
            : new ToDoService(client, path, new QToDoId(fieldName).buildUrl(id));
    };
    return ToDosApiModelsService;
}(ODataService));
export { ToDosApiModelsService };
