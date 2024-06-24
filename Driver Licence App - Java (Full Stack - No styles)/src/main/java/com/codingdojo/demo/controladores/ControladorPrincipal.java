package com.codingdojo.demo.controladores;

import java.util.List;

import javax.validation.Valid;

import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.validation.BindingResult;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;

import com.codingdojo.demo.modelos.Licencia;
import com.codingdojo.demo.modelos.Persona;
import com.codingdojo.demo.servicios.ServicioLicencias;

@Controller
public class ControladorPrincipal {
	private final ServicioLicencias servicio;
	public ControladorPrincipal(ServicioLicencias servicio) {
		this.servicio = servicio;
	}
	
	@GetMapping("/")
	public String Index(Model model) {
		List<Persona> personas = servicio.todasPersonas();
		model.addAttribute("personas",personas);
		return "index.jsp";
	}
	
	@GetMapping("/nueva")
	public String nuevaPersona(@ModelAttribute("persona") Persona persona) {
		return "nuevaPersona.jsp";		
	}
	
	@PostMapping("/persona")
	public String guardarPersona(@Valid @ModelAttribute("persona") Persona persona, BindingResult result) {
		if(result.hasErrors()) {
			return "nuevaPersona.jsp";
		}
		servicio.guardarPersona(persona);
		return "redirect:/";
	}
	
	@GetMapping("/licencia")
	public String nuevaLicencia(@ModelAttribute("licencia")Licencia licencia, Model model) {
		List<Persona> sinLicencia = servicio.sinLicencia();
		model.addAttribute("persona", sinLicencia);
		return "nuevaLicencia.jsp";
	}
	
	@PostMapping("/licencia")
	public String guardarLicencia(@Valid @ModelAttribute("licencia")Licencia licencia, BindingResult result) {
		if(result.hasErrors())
			return "nuevaLicencia.jsp";
		servicio.crearLicencia(licencia);
		return "redirect:/";
	}
	
	@GetMapping("/informacion/{id}")
	public String ShowPerson(@PathVariable("id") Long identificador, Model model) {
		model.addAttribute("persona", servicio.obtenerPersona(identificador));
		return "informacion.jsp";
	}
	
}
