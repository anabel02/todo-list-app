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
import { EntityTypeServiceV4, EntitySetServiceV4 } from "@odata2ts/odata-service";
import { qToDo, QToDoId } from "../QToDosApiModels";
var ToDoService = /** @class */ (function (_super) {
    __extends(ToDoService, _super);
    function ToDoService(client, basePath, name) {
        return _super.call(this, client, basePath, name, qToDo) || this;
    }
    return ToDoService;
}(EntityTypeServiceV4));
export { ToDoService };
var ToDoCollectionService = /** @class */ (function (_super) {
    __extends(ToDoCollectionService, _super);
    function ToDoCollectionService(client, basePath, name) {
        return _super.call(this, client, basePath, name, qToDo, new QToDoId(name)) || this;
    }
    return ToDoCollectionService;
}(EntitySetServiceV4));
export { ToDoCollectionService };
