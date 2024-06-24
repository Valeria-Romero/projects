package com.codingdojo.demo.repositorios;

import java.util.List;

import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;

import com.codingdojo.demo.modelos.Licencia;

@Repository
public interface RepoLicencia extends CrudRepository <Licencia, Long>{
		public List<Licencia> findAll();
		public Licencia findTopByOrderByNumeroDesc();
		
}
