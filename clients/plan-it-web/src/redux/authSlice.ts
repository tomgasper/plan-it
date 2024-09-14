import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { UserFromJwt } from '../types/User';
interface AuthState {
    user: UserFromJwt | null;
    token: string | null;
    isAuthenticated: boolean;
    isLoading: boolean;
}

const initialState: AuthState = {
    user: null,
    token: localStorage.getItem('token'),
    isAuthenticated: false,
    isLoading: true
};

const authSlice = createSlice({
    name: 'auth',
    initialState,
    reducers: {
        setCredentials: (state, action: PayloadAction<{ user: UserFromJwt; token: string }>) => {
            const { user, token } = action.payload;
            state.user = user;
            state.token = token;
            state.isAuthenticated = true;
            localStorage.setItem('token', token);
        },
        logOut: (state) => {
            state.user = null;
            state.token = null;
            state.isAuthenticated = false;
            localStorage.removeItem('token');
        },
        setAuthLoading: (state, action: PayloadAction<boolean>) => {
            state.isLoading = action.payload;
          },
        setAuthState: (state, action: PayloadAction<{ isAuthenticated: boolean; isLoading: boolean }>) => {
        state.isAuthenticated = action.payload.isAuthenticated;
        state.isLoading = action.payload.isLoading;
        },
    }
});

export const { setCredentials, logOut, setAuthLoading, setAuthState } = authSlice.actions;

export default authSlice.reducer;
