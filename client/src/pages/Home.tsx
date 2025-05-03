import { useEffect, useRef, useState } from 'react';
import { fetchCustomers, deleteCustomer } from '../api/api';
import { useNavigate } from 'react-router-dom';
import { Customer } from '../types/models';

export default function Home() {
  const [customers, setCustomers] = useState<Customer[]>([]);
  const [originalCustomers, setOriginalCustomers] = useState<Customer[]>([]);
  const navigate = useNavigate();
  const loaded = useRef(false);


  
  const loadCustomers = async () => {
    const res = await fetchCustomers();
    setOriginalCustomers(res.data.result);
  };


  useEffect(() => {
    setCustomers(originalCustomers);
  }, [originalCustomers]);


  useEffect(() => {
    if (!loaded.current) {
      loaded.current = true;
      loadCustomers();
    }
  }, []);


  const filterCustomers = (filterBy: string)  => {
    const filteredCustomers = originalCustomers
    .filter(c => 
        c.email.includes(filterBy) ||
        c.fullName.includes(filterBy) ||
        c.phone.includes(filterBy));

    setCustomers(filteredCustomers);
    }

  return (
    <div>
      <h2>Customers</h2>
      <div>
        <label>filter</label>
        <input  onChange={e => filterCustomers(e.target.value)}></input>
      </div>
      <table border={1}>
        <thead>
          <tr>
            <th>Full Name</th><th>Email</th><th>Phone</th><th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {customers.map(c => (
            <tr key={c.id}>
              <td>{c.fullName}</td>
              <td>{c.email}</td>
              <td>{c.phone}</td>
              <td>
                <button onClick={() => navigate(`/edit/${c.id}`)}>Edit</button>
                <button onClick={async () => { await deleteCustomer(c.id); loadCustomers(); }}>
                  Delete
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
