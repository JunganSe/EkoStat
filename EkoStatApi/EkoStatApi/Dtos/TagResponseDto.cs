﻿namespace EkoStatApi.Dtos;

internal class TagResponseDto
{
    public string? Name { get; set; } = null!;
    public List<int>? ArticleIds { get; set; }
    public int? UserId { get; set; }
}