package com.codingdojo.demo.modelos;

import java.text.SimpleDateFormat;
import java.util.Date;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.FetchType;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.OneToOne;
import javax.persistence.PostPersist;
import javax.persistence.PrePersist;
import javax.persistence.Table;

import org.springframework.format.annotation.DateTimeFormat;

@Entity
@Table(name="licencias")
public class Licencia {

	@Id
	@GeneratedValue(strategy=GenerationType.IDENTITY)
	private Long id;
	private Integer numero;
	@DateTimeFormat(pattern="yyyy-MM-dd")
	private Date fechaExpiracion;
	private String estado;
	@Column(updatable=false)
	@DateTimeFormat(pattern="yyyy-MM-dd")
	private Date creado;
	@DateTimeFormat(pattern="yyyy-MM-dd")
	private Date actualizado;
	
	@OneToOne(fetch=FetchType.LAZY)
	@JoinColumn(name="persona_id")
	private Persona persona;
	
	public Licencia() {
		
	}
	
	public String formatoNumeroLicencia() {
		int numCeros = 7 - String.valueOf(this.numero).length();
		StringBuilder nuevoNumero = new StringBuilder();
		for(int i = 0; i < numCeros; i++)
			nuevoNumero.append('0');
		return String.format("%s%d", nuevoNumero, this.numero);
	}
	
	public String formatoFechaExpiracion() {
		SimpleDateFormat formatoFecha = new SimpleDateFormat("MM/dd/yyyy");
		return formatoFecha.format(fechaExpiracion);
	}

	public Long getId() {
		return id;
	}

	public void setId(Long id) {
		this.id = id;
	}

	public Integer getNumero() {
		return numero;
	}

	public void setNumero(Integer numero) {
		this.numero = numero;
	}

	public Date getFechaExpiracion() {
		return fechaExpiracion;
	}

	public void setFechaExpiracion(Date fechaExpiracion) {
		this.fechaExpiracion = fechaExpiracion;
	}

	public String getEstado() {
		return estado;
	}

	public void setEstado(String estado) {
		this.estado = estado;
	}

	public Date getCreado() {
		return creado;
	}

	public void setCreado(Date creado) {
		this.creado = creado;
	}

	public Date getActualizado() {
		return actualizado;
	}

	public void setActualizado(Date actualizado) {
		this.actualizado = actualizado;
	}

	public Persona getPersona() {
		return persona;
	}

	public void setPersona(Persona persona) {
		this.persona = persona;
	}
	
	@PrePersist
	protected void onCreado() {
		this.creado = new Date();
	}
	@PostPersist
	protected void onActualizado() {
		this.actualizado = new Date();
	}
}
