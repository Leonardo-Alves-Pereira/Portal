﻿namespace Portal.Domain.Entidade;

public class Usuario : EntidadeBase
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public string Telefone { get; set; }
}