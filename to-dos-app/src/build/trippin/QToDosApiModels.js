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
import { QNumberPath, QStringPath, QDateTimeOffsetPath, QId, QNumberParam, QueryObject } from "@odata2ts/odata-query-objects";
var QToDo = /** @class */ (function (_super) {
    __extends(QToDo, _super);
    function QToDo() {
        var _this = _super !== null && _super.apply(this, arguments) || this;
        _this.id = new QNumberPath(_this.withPrefix("Id"));
        _this.task = new QStringPath(_this.withPrefix("Task"));
        _this.createdDateTime = new QDateTimeOffsetPath(_this.withPrefix("CreatedDateTime"));
        _this.completedDateTime = new QDateTimeOffsetPath(_this.withPrefix("CompletedDateTime"));
        return _this;
    }
    return QToDo;
}(QueryObject));
export { QToDo };
export var qToDo = new QToDo();
var QToDoId = /** @class */ (function (_super) {
    __extends(QToDoId, _super);
    function QToDoId() {
        var _this = _super !== null && _super.apply(this, arguments) || this;
        _this.params = [new QNumberParam("Id", "id")];
        return _this;
    }
    QToDoId.prototype.getParams = function () {
        return this.params;
    };
    return QToDoId;
}(QId));
export { QToDoId };
