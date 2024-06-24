<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
    pageEncoding="ISO-8859-1"%>
<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<!DOCTYPE html>
<html>
<head>
<meta charset="ISO-8859-1">
<title>Licencia de manejo</title>
</head>
<body>
	<h1>Bienvenidos a licencias</h1>
	<a href="/licencia">Nueva licencia</a>
	<a href="/nueva">Nueva persona</a>
	
	<table>
			<thead>
				<tr>
					<th>Nombre</th>
					<th>Licencia</th>	
				</tr>
			</thead>
			<tbody>
			<c:forEach items="${ personas }" var="persona">
				<tr>
					<td><a href="/informacion/${persona.id}">${ persona.nombre } ${ persona.apellido }</a></td>
					<td>${ persona.licencia.formatoNumeroLicencia()}</td>
				</tr>
			</c:forEach>
			</tbody>
		</table>	
</body>
</html>