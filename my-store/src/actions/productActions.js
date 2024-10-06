import axios from 'axios';
import { fetchProductsSuccess, fetchProductsFail } from '../reducers/productReducer';

export const fetchProducts = () => async (dispatch) => {
  try {
    const response = await axios.get('http://localhost:5000/products');
    dispatch(fetchProductsSuccess(response.data));
  } catch (error) {
    dispatch(fetchProductsFail(error.message));
  }
};
