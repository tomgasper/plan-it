import { useEffect } from 'react';
import { useAppDispatch, useAppSelector } from './reduxHooks';
import { setCredentials, logOut, setAuthState } from '../redux/authSlice';
import { JwtInformation } from '../types/Auth';
import { jwtDecode } from 'jwt-decode';

// Hook that checks if the user is authenticated based on JWT token received from the server
export const useJwtAuth = () => {
  const { isAuthenticated, isLoading } = useAppSelector((state) => state.auth);
  const dispatch = useAppDispatch();

  useEffect(() => {
      const token = localStorage.getItem('token');
      if (token) {
        try {
          const decodedToken: JwtInformation = jwtDecode(token);
  
          const userFromToken = {
            id: decodedToken.sub,
            firstName: decodedToken.given_name,
            lastName: decodedToken.family_name,
          };
          
          if (decodedToken.exp && decodedToken.exp * 1000 > Date.now()) {
            dispatch(setCredentials({ token, user: userFromToken }));
            dispatch(setAuthState({ isAuthenticated: true, isLoading: false }));
          } else {
            dispatch(logOut());
            dispatch(setAuthState({ isAuthenticated: false, isLoading: false }));
          }
        } catch (err) {
          console.error('Invalid token:', err);
          dispatch(logOut());
          dispatch(setAuthState({ isAuthenticated: false, isLoading: false }));
        }
      } else {
        dispatch(setAuthState({ isAuthenticated: false, isLoading: false }));
      }

      
    
  }, [dispatch]);

  return { isAuthenticated, isLoading };
};
