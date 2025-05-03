import { useEffect, useRef, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { getCustomerById, updateCustomer } from '../api/api';
import { CustomerDto } from '../types/models';

export default function EditCustomer() {
    const { id } = useParams();
    const [form, setForm] = useState<CustomerDto>({ fullName: '', email: '', phone: '' });
    const navigate = useNavigate();
    const loaded = useRef(false);

    useEffect(() => {
        if (id && !loaded.current) {
            loaded.current = true;
            getCustomerById(+id).then(res => setForm(res.data.result));
        }
    }, [id]);

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        if (id) {
            await updateCustomer(+id, form);
            navigate('/home');
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            {form.fullName} 123
            <h2>Edit Customer</h2>
            <input value={form.fullName} onChange={e => setForm({ ...form, fullName: e.target.value })} />
            <input value={form.email} onChange={e => setForm({ ...form, email: e.target.value })} />
            <input value={form.phone} onChange={e => setForm({ ...form, phone: e.target.value })} />
            <button type="submit">Save</button>
        </form>
    );
}
