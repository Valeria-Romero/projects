package com.codingdojo.demo.servicios;

import java.util.List;

import org.springframework.stereotype.Service;

import com.codingdojo.demo.modelos.Licencia;
import com.codingdojo.demo.modelos.Persona;
import com.codingdojo.demo.repositorios.RepoLicencia;
import com.codingdojo.demo.repositorios.RepoPersona;

@Service
public class ServicioLicencias {
	private final RepoPersona repoPersona;
	private final RepoLicencia repoLicencia;
	public ServicioLicencias(RepoPersona rPersona, RepoLicencia rLicencia) {
		this.repoPersona = rPersona;
		this.repoLicencia = rLicencia;
	}
	
	public Persona guardarPersona(Persona persona) {
		return repoPersona.save(persona);
	}
	
	public List<Persona> sinLicencia(){
		return repoPersona.findByLicenciaIdIsNull();
	}
	
	public int generarNumeroLicencia() {
		Licencia licencia = repoLicencia.findTopByOrderByNumeroDesc();
		if(licencia == null)
			return 1;
		int numeroMayor = licencia.getNumero();
		numeroMayor++;
		return(numeroMayor);
	}
	
	public Licencia crearLicencia(Licencia licencia) {
		licencia.setNumero(this.generarNumeroLicencia());
		return repoLicencia.save(licencia);
	}
	
	public List<Persona> todasPersonas(){
		return repoPersona.findAll();
	}
	
	public Persona obtenerPersona(Long id) {
		return repoPersona.findById(id).orElse(null);
	}
}
