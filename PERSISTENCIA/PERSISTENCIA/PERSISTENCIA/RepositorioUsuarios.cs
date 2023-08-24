using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using PERSISTENCIA;
/*

namespace PERSISTENCIA.PERSISTENCIA;

public class RepositorioUsuarios
{
  
       // public ArrayList usuarios;
    public ArrayList usuarios;
   

    //REPOSITORIO-------------------------------------------------------------
    public RepositorioUsuarios()
        {
        usuarios = new ArrayList();
        usuarios.Add(new Usuario(1, "PRUEBA", "prueba", "OPERADOR"));
        usuarios.Add(new Usuario(2, "TEST", "9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08", "OPERADOR"));
        usuarios.Add(new Usuario(3, "AA", "961b6dd3ede3cb8ecbaacbd68de040cd78eb2ed5889130cceb4c49268ea4d506", "ADMINISTRADOR"));
        usuarios.Add(new Usuario(4, "AAA", "aaa", "ADMINISTRADOR"));
        usuarios.Add(new Usuario(5, "MARIA", "Maria", "OPERADOR"));
        usuarios.Add(new Usuario(6, "LUCIA", "Lucia", "OPERADOR"));
        usuarios.Add(new Usuario(7, "MATEO", "Mateo", "OPERADOR"));
        usuarios.Add(new Usuario(8, "MARTIN", "Martin", "OPERADOR"));
        usuarios.Add(new Usuario(9, "LUCAS", "Lucas" , "OPERADOR"));
        usuarios.Add(new Usuario(10, "MATI", "Mati" , "OPERADOR"));
        usuarios.Add(new Usuario(11, "MATIAS", "Matias" , "OPERADOR"));
       
    }
    
    //BUSCAR USUARIO-----------------------------------------
    public Usuario buscarUsuario(string login)
    {
        foreach (Usuario usr in usuarios)
        {
            if (usr.login.Equals(login))
            {
                //es una buena practica logear a la consola
                Console.WriteLine("Usuario existe " + login);
                return usr;
            }
        }
        //esto lo vamos a mejorar en futuras versiones
        return null;
    }

    //DEVUELVE LOS USUARIOS -----------------------------------------------
    public ArrayList buscarTodos()
    {
        return usuarios;
    }


    //GUARDAR USUARIOS------------------------------------------------------------
    public void GuardarUsuario(Usuario usuario)
    {
        // Convertir el login a mayúsculas
        usuario.login = usuario.login.ToUpper();

        // Verificar si el usuario ya existe en la lista por su login
        foreach (Usuario usr in usuarios)
        {
            if (usr.login.Equals(usuario.login))
            {
                throw new Exception("El usuario ya existe.");
            }
        }

        usuario.id = usuarios.Count + 1; // Asignar el ID al nuevo usuario
        usuarios.Add(usuario); // Agregar el usuario al final de la lista

    }


    public Usuario buscarUsuarioPorId(int id)
    {
        foreach (Usuario usr in usuarios)
        {
            if (usr.id == id)
            {

                return usr;
            }
        }
        //esto lo vamos a mejorar en futuras versiones
        return null;
    }


    public bool borrarUsuario(int idUsuario)
    {
        foreach (Usuario usr in usuarios)
        {
            if (usr.id == idUsuario)
            {
                usuarios.Remove(usr);
                ReasignarIndices();
                return true;
            }
        }

        return false;
    }

    private void ReasignarIndices()
    {
        int newId = 1;
        ArrayList nuevosUsuarios = new ArrayList();

        foreach (Usuario usuario in usuarios)
        {
            usuario.id = newId;
            nuevosUsuarios.Add(usuario);
            newId++;
        }

        usuarios = nuevosUsuarios;
    }




   




    //fin-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*
}


    */