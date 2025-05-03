import { useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import * as yup from 'yup';
import { getCustomerById, updateCustomer } from '../api/api';
import { CustomerDto } from '../types/models';

// 1. Define validation schema
const schema = yup.object().shape({
  fullName: yup.string().required('Full name is required'),
  email: yup.string().email('Invalid email').required('Email is required'),
  phone: yup.string().required('Phone is required'),
});

export default function EditCustomer() {
  const { id } = useParams();
  const navigate = useNavigate();


  const {
    register,
    handleSubmit,
    setValue,
    formState: { errors },
  } = useForm<CustomerDto>({
    resolver: yupResolver(schema),
    defaultValues: { fullName: '', email: '', phone: '' },
  });


  useEffect(() => {
    if (id) {
      getCustomerById(+id).then(res => {
        const customer = res.data.result;
        setValue('fullName', customer.fullName);
        setValue('email', customer.email);
        setValue('phone', customer.phone);
      });
    }
  }, [id, setValue]);


  const onSubmit = async (data: CustomerDto) => {
    if (id) {
      await updateCustomer(+id, data);
      navigate('/home');
    }
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)}>
      <h2>Edit Customer</h2>

      <div>
        <label>Full Name:</label>
        <input {...register('fullName')} />
        <p style={{ color: 'red' }}>{errors.fullName?.message}</p>
      </div>

      <div>
        <label>Email:</label>
        <input {...register('email')} />
        <p style={{ color: 'red' }}>{errors.email?.message}</p>
      </div>

      <div>
        <label>Phone:</label>
        <input {...register('phone')} />
        <p style={{ color: 'red' }}>{errors.phone?.message}</p>
      </div>

      <button type="submit">Save</button>
    </form>
  );
}
