import { createSlice } from '@reduxjs/toolkit';

const authSlice = createSlice({
  name: 'auth',
  initialState: {
    isAuthenticated: false,
    loading: false,
    error: null,    
    userId: null,
    token: null,
  },
  reducers: {
    loginSuccess: (state, action) => {
      state.token = action.payload.token;
      state.userId = action.payload.userId;
      state.isAuthenticated = true;
      state.loading = false;
      state.error = null;
    },
    loginFail: (state, action) => {
      state.token = null;
      state.userId = null;
      state.isAuthenticated = false;
      state.loading = false;
      state.error = action.payload;
    },
    registerSuccess: (state) => {
      state.isAuthenticated = false;
      state.loading = false;
      state.error = null;
    },
    registerFail: (state, action) => {
      state.isAuthenticated = false;
      state.loading = false;
      state.error = action.payload;
    },
    logout: (state) => {
      state.token = null;
      state.userId = null;
      state.isAuthenticated = false;
      state.loading = false;
    },
  },
});

export const { loginSuccess, loginFail, registerSuccess, registerFail, logout } = authSlice.actions;
export default authSlice.reducer;
