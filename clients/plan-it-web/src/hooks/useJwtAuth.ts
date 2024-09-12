import { useEffect } from 'react';
import { useAppDispatch, useAppSelector } from '../hooks/reduxHooks';
import { setCredentials, logOut } from '../redux/authSlice';
import { JwtInformation } from '../types/Auth';
import { jwtDecode } from 'jwt-decode';

// Hook that checks if the user is authenticated based on JWT token received from the server
export const useJwtAuth = () => {
  const dispatch = useAppDispatch();
  const isAuthenticated = useAppSelector(state => state.auth.isAuthenticated);

  useEffect(() => {
    const token = localStorage.getItem('token');
    
    if (token) {
      try {
        const decodedToken: JwtInformation = jwtDecode(token);

        const userFromToken = {
          id: decodedToken.jti,
          firstName: decodedToken.given_name,
          lastName: decodedToken.family_name,
        };
        
        // Sprawdzenie, czy token jest nadal ważny
        if (decodedToken.exp && decodedToken.exp * 1000 > Date.now()) {
          dispatch(setCredentials({ token, user: userFromToken }));
        } else {
          // Token wygasł
          dispatch(logOut());
        }
      } catch (err) {
        console.error('Invalid token:', err);
        dispatch(logOut());
      }
    }
  }, [dispatch]);

  return isAuthenticated;
};
