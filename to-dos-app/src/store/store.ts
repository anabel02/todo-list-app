import { configureStore } from "@reduxjs/toolkit";
import thunk from "redux-thunk";
import { useDispatch } from "react-redux";
import { todoReducer } from "./todoReducer";

export const store = configureStore({
  reducer: todoReducer,
  middleware: (getDefaultMiddleware) => getDefaultMiddleware().concat(thunk),
});

export type RootState = ReturnType<typeof todoReducer>;

export type AppDispatch = typeof store.dispatch;
export const useAppDispatch = () => useDispatch<AppDispatch>();
export default store;