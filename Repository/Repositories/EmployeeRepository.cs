using System;
using System.Collections.Generic;
using System.Data;
using Core.Abstractions.Repositories;
using Core.Entities;
using Dapper.Contrib.Extensions;

namespace Repository.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IDbConnection _connection;
        private IDbTransaction _transaction;

        public EmployeeRepository(IDbConnection connection)
        {
            _connection = connection;
        }


        public IEnumerable<Employee> Retrieve()
        {
            IEnumerable<Employee> employees;

            using (_connection)
            {
                _connection.Open();
                employees = _connection.GetAll<Employee>();
            }

            return employees;
        }

        public Employee Retrieve(int id)
        {
            Employee employee = new Employee();

            using (_connection)
            {
                _connection.Open();
                employee = _connection.Get<Employee>(id);
            }

            return employee;
        }

        public void Create(Employee employee)
        {
            using (_connection)
            {
                try
                {
                    _connection.Open();
                    _connection.Insert<Employee>(employee);
                    _transaction.Commit();
                }
                catch (Exception ex)
                {
                    _transaction.Rollback();
                    throw ex;
                }
                finally
                {
                    _transaction.Dispose();
                    _connection.Close();
                }
            }
        }

        public void Update(Employee employee)
        {
            using (_connection)
            {
                try
                {
                    _connection.Open();
                    _connection.Update<Employee>(employee);
                    _transaction.Commit();
                }
                catch (Exception ex)
                {
                    _transaction.Rollback();
                    throw ex;
                }
                finally
                {
                    _transaction.Dispose();
                    _connection.Close();
                }
            }
        }

        public void Delete(int id)
        {
            using (_connection)
            {
                try
                {
                    _connection.Open();
                    _connection.Delete<Employee>(new Employee { Id = id });
                    _transaction.Commit();
                }
                catch (Exception ex)
                {
                    _transaction.Rollback();
                    throw ex;
                }
                finally
                {
                    _transaction.Dispose();
                    _connection.Close();
                }
            }
        }
    }
}