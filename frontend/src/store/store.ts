import { configureStore } from '@reduxjs/toolkit';
import loadingReducer from './slices/loadingSlice.ts';
import windowSearchReducer from './slices/windowSearchSlice.ts';
import filterReducer from './slices/filtersSlice.ts';

export const store = configureStore({
  reducer: {
    loading: loadingReducer,
    windowSearch: windowSearchReducer,
    filters: filterReducer,
  },
});

// Infer the `RootState` and `AppDispatch` types from the store itself
export type RootState = ReturnType<typeof store.getState>;
// Inferred type: {posts: PostsState, comments: CommentsState, users: UsersState}
export type AppDispatch = typeof store.dispatch;
