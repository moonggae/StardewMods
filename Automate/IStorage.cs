using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Pathoschild.Stardew.Automate;

/// <summary>Manages access to items in the underlying containers.</summary>
public interface IStorage
{
    /*********
    ** Accessors
    *********/
    /// <summary>The storage containers that accept input, in priority order.</summary>
    IContainer[] InputContainers { get; }

    /// <summary>The storage containers that provide items, in priority order.</summary>
    IContainer[] OutputContainers { get; }


    /*********
    ** Public methods
    *********/
    /// <summary>Get whether any of the <see cref="InputContainers"/> or <see cref="OutputContainers"/> are locked.</summary>
    bool HasLockedContainers();

    /// <summary>Get all items from the given pipes.</summary>
    IEnumerable<ITrackedStack> GetItems();

    /****
    ** TryGetIngredient
    ****/
    /// <summary>Get an ingredient needed for a recipe.</summary>
    /// <param name="predicate">Returns whether an item should be matched.</param>
    /// <param name="count">The number of items to find.</param>
    /// <param name="consumable">The matching consumables.</param>
    /// <returns>Returns whether the requirement is met.</returns>
    bool TryGetIngredient(Func<ITrackedStack, bool> predicate, int count, [NotNullWhen(true)] out IConsumable? consumable);

    /// <summary>Get an ingredient needed for a recipe.</summary>
    /// <param name="recipes">The items to match.</param>
    /// <param name="consumable">The matching consumables.</param>
    /// <param name="recipe">The matched requisition.</param>
    /// <returns>Returns whether the requirement is met.</returns>
    bool TryGetIngredient(IRecipe[] recipes, [NotNullWhen(true)] out IConsumable? consumable, [NotNullWhen(true)] out IRecipe? recipe);

    /****
    ** TryConsume
    ****/
    /// <summary>Consume an ingredient needed for a recipe.</summary>
    /// <param name="predicate">Returns whether an item should be matched.</param>
    /// <param name="count">The number of items to find.</param>
    /// <returns>Returns whether the item was consumed.</returns>
    bool TryConsume(Func<ITrackedStack, bool> predicate, int count);

    /****
    ** TryPush
    ****/
    /// <summary>Add the given item stack to the output pipe if there's space.</summary>
    /// <param name="item">The item stack to push.</param>
    /// <returns>Returns whether at least some of the item stack was received.</returns>
    bool TryPush(ITrackedStack? item);
}
