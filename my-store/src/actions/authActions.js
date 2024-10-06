import axios from 'axios';
import { jwtDecode } from 'jwt-decode';
import { loginSuccess, loginFail, registerSuccess, registerFail, logout } from '../reducers/authReducer';

export const loginUser = (email, password, navigate) => async (dispatch) => {
  try {
    const response = await axios.post('http://localhost:5000/auth/login', { email, password });
    const { token } = response.data;
    const decodedToken = jwtDecode(token);
    localStorage.setItem('token', token);
    localStorage.setItem('userId', decodedToken.userId);
    await dispatch(loginSuccess({ token, userId: decodedToken.userId }));
    navigate('/');
  } catch (error) {
    let errorMessage = 'Неизвестная ошибка'; 

    if (error.response) {
      if (error.response.status === 401) {
        errorMessage = 'Неверные учетные данные. Пожалуйста, проверьте ваш email и пароль.';
      } else {
        errorMessage = error.response.data.message || 'Произошла ошибка при входе. Попробуйте снова.';
      }
    }
    dispatch(loginFail(errorMessage));
  }
};

export const registerUser = (email, password, navigate) => async (dispatch) => {
  try {
    await axios.post('http://localhost:5000/auth/register', { email, password });
    await dispatch(registerSuccess());
    alert('Registration successful! Please log in.');
    navigate('/login');
  } catch (error) {
    dispatch(registerFail(error.message));
  }
};

export const logoutUser = () => (dispatch) => {
  localStorage.removeItem('token');
  localStorage.removeItem('userId');
  dispatch(logout());
};
