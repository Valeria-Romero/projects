package com.codingdojo.demo.repositorios;

import java.util.List;

import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;

import com.codingdojo.demo.modelos.Persona;

@Repository
public interface RepoPersona extends CrudRepository<Persona, Long>{
	List<Persona> findAll();
	List<Persona> findByLicenciaIdIsNull();
	@Query(value="SELECT personas.* FROM personas LEFT OUTER JOIN licencias ON personas.id = licencias.persona_id WHERE licencias.id IS NULL", nativeQuery=true)
	List<Persona> findBySinLicencia();
}
